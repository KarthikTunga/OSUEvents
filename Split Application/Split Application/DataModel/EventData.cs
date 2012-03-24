using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace OSUEvents
{

    /// <summary>
    /// Base class for <see cref="EventDataItem"/> and <see cref="EventDataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class EventDataCommon : OSU_Events_App.Common.BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public EventDataCommon(String uniqueId, String title, String subtitle, String imagePath, String description)
        {
            this._uniqueId = uniqueId;
            this._title = title;
            this._subtitle = subtitle;
            this._description = description;
            this._imagePath = imagePath;
        }

        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get { return this._subtitle; }
            set { this.SetProperty(ref this._subtitle, value); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private ImageSource _image = null;
        private String _imagePath = null;
        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(EventDataCommon._baseUri, this._imagePath));
                }
                return this._image;
            }

            set
            {
                this._imagePath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }
    }

    //Holds info for a single Event Source feed
    public class EventsData
    {
        public string source;
        private List<EventItem> _Items = new List<EventItem>();

        public List<EventItem> Items
        {
            get
            {
                return this._Items;
            }
        }
    }

    //Holds info for a single Event
    public class EventItem:EventDataCommon
    {
        //Windows.Data.Json.JsonObject myobj = new JsonObject();
        private DateTime _startdate;
        private DateTime _enddate;
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string eventtype { get; set; }
        public string eventlink { get; set; }
        public string detailslink { get; set; }
        public string description { get; set; }
        public string contactname { get; set; }
        public string contactemail { get; set; }
        public string contactnumber { get; set; }
        public string location { get; set; }
        private string _content = string.Empty;
        public EventItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content, EventsDataGroup group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this._content = content;
            this._group = group;
        }

        
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private EventsDataGroup _group;
        public EventsDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
        
        public string startdate
        {
            get
            {
                return _startdate.ToString();
            }
            set
            {
                DateTime.TryParse(value, out _startdate);
            }
        }
        public string enddate
        {
            get
            {
                return _enddate.ToString();
            }
            set
            {
                DateTime.TryParse(value, out _enddate);
            }
        }

    }
    public class EventsDataGroup:EventDataCommon
    {
        public EventsDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            WrapperConstructor();
        }

        private ObservableCollection<EventItem> _items = new ObservableCollection<EventItem>();
        public ObservableCollection<EventItem> Items
        {
            get { return this._items; }
        }
        public async Task WrapperConstructor()
        {
            await getEventsAsync();
        }
        public async Task getEventsAsync()
        {
            string url1 = "http://5.130.180.248:3000/get_events";
            foreach (EventItem eventItem in await GetEventsFeedData(url1,this,true))
            {
                this._items.Add(eventItem);
            }
        }

        public async Task<string> GetJsonString(string url)
        {
            HttpClient myclient = new System.Net.Http.HttpClient();

            Uri myuri = new Uri(url);
            HttpResponseMessage response = await myclient.GetAsync(myuri);
            string resstr = await response.Content.ReadAsStringAsync();
            return resstr;
        }
        // have to remote the dummy data later.
        public async Task<List<EventItem>> GetEventsFeedData(string url,EventsDataGroup group, bool dummyData)
        {
            if (dummyData == true)
            {
                List<EventItem> listOfEvents = new List<EventItem>();
                for (int i = 1; i < 100; ++i)
                {
                    EventItem eventItem = new EventItem("Event id - " + i, "Event id - " + i, "Event "+i+" Subtitle", "Assets/event.jpg", "Test Event", "This is a dummy event organized by hackathon", group);
                    listOfEvents.Add(eventItem);
                }
                return listOfEvents;
            }

            char[] trimchars = { '\"', ' ', '\t', '\n' };
            try
            {
                string jsonstr = await GetJsonString(url);
                JsonObject myobj = JsonObject.Parse(jsonstr);
                JsonObject myres = myobj.GetNamedObject("result");
                bool status = myres.GetNamedBoolean("status");
                JsonArray data = myres.GetNamedArray("data");

                List<EventItem> listOfEvents = new List<EventItem>();

                if (status)
                {
                    //get the list of events
                    foreach (var item in data)
                    {
                        //get the list of key, value pairs in each event
                        Dictionary<string, string> tempdict = new Dictionary<string, string>();
                        foreach (var details in item.GetObject())
                        {
                            //textBox1.Text += details.Key + " " + details.Value.Stringify() + "\n";
                            tempdict.Add(details.Key, details.Value.Stringify());
                        }

                        EventItem tempitem = new EventItem("","","","","","",group);
                        bool res;
                        string resstr;
                        #region regionEventDetails
                        res = tempdict.TryGetValue("category", out resstr);
                        if (res) { tempitem.category = resstr; } resstr = null;

                        res = tempdict.TryGetValue("contactemail", out resstr);
                        if (res) { tempitem.contactemail = resstr; } resstr = null;

                        res = tempdict.TryGetValue("contactname", out resstr);
                        if (res) { tempitem.contactname = resstr; } resstr = null;

                        res = tempdict.TryGetValue("contactnumber", out resstr);
                        if (res) { tempitem.contactnumber = resstr; } resstr = null;

                        res = tempdict.TryGetValue("description", out resstr);
                        if (res) { tempitem.description = resstr; } resstr = null;

                        res = tempdict.TryGetValue("detailslink", out resstr);
                        if (res) { tempitem.detailslink = resstr; } resstr = null;

                        res = tempdict.TryGetValue("end_date", out resstr);
                        if (resstr != "null" && res)
                        {
                            tempitem.enddate = resstr.Trim(trimchars);
                        }
                        resstr = null;

                        res = tempdict.TryGetValue("start_date", out resstr);
                        if (resstr != "null" && res)
                        {
                            tempitem.startdate = resstr.Trim(trimchars);
                        }
                        resstr = null;

                        res = tempdict.TryGetValue("id", out resstr);
                        if (res) { tempitem.id = Convert.ToInt32(resstr); } resstr = null;

                        res = tempdict.TryGetValue("eventlink", out resstr);
                        if (res) { tempitem.eventlink = resstr; } resstr = null;

                        res = tempdict.TryGetValue("eventtype", out resstr);
                        if (res) { tempitem.eventtype = resstr; } resstr = null;

                        res = tempdict.TryGetValue("location", out resstr);
                        if (res) { tempitem.location = resstr; } resstr = null;

                        res = tempdict.TryGetValue("name", out resstr);
                        if (res) { tempitem.name = resstr.Trim(trimchars); } resstr = null;


                        /*
                        //tempitem.contactemail = tempdict["contactemail"];
                        //tempitem.contactname = tempdict["contactname"];
                        //tempitem.contactnumber = tempdict["contactnumber"];
                        //tempitem.description = tempdict["description"];
                        //tempitem.detailslink = tempdict["detailslink"];
                        //tempitem.enddate = tempdict["enddate"];
                        //tempitem.id = Convert.ToInt32(tempdict["id"]);
                        //tempitem.eventlink = tempdict["eventlink"];
                        //tempitem.eventtype = tempdict["eventtype"];
                        //tempitem.location = tempdict["location"];
                        //tempitem.name = tempdict["name"];
                        //tempitem.startdate = tempdict["startdate"];
                        //textBox1.Text += "\n";*/
                        #endregion
                        listOfEvents.Add(tempitem);
                    }
                }
                return listOfEvents;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }

    // Holds a collection of Event Sources
    public class EventsDataSource
    {
        private ObservableCollection<EventsDataGroup> _EventsGroup = new ObservableCollection<EventsDataGroup>();
        public ObservableCollection<EventsDataGroup> EventsGroup
        {
            get
            {
                return this._EventsGroup;
            }
        }
        public EventsDataSource()
        {
            var group1 = new EventsDataGroup("Wexner Art & Science",
                "Wexner Art & Science",
                "",
                "Assets/wexner.jpg",
                "Events at Wexner");
            this._EventsGroup.Add(group1);
            var group2 = new EventsDataGroup("Computer Science",
                "Computer Science",
                "",
                "Assets/DL.jpg",
                "Events at Wexner");
            this._EventsGroup.Add(group2);
        }
    }

    public class CategoryData
    {
        private List<CategoryItem> _Items = new List<CategoryItem>();

        public List<CategoryItem> Items
        {
            get
            {
                return this._Items;
            }
        }
    }

    public class CategoryDataSource
    {
        private ObservableCollection<CategoryData> _Categories = new ObservableCollection<CategoryData>();

        public ObservableCollection<CategoryData> Categories
        {
            get
            {
                return this._Categories;
            }
        }

        public async Task getCategoriesAsync()
        {
            string url1 = "http://5.130.180.248:3000/get_event_categories";
            CategoryData feed1 = await GetCategoriesData(url1);
            this.Categories.Add(feed1);
        }

        public async Task<string> GetJsonString(string url)
        {
            HttpClient myclient = new System.Net.Http.HttpClient();
            Uri myuri = new Uri(url);
            HttpResponseMessage response = await myclient.GetAsync(myuri);
            string resstr = await response.Content.ReadAsStringAsync();
            return resstr;
        }

        public async Task<CategoryData> GetCategoriesData(string url)
        {
            char[] trimchars = { '\"', ' ', '\t', '\n' };
            try
            {
                string jsonstr = await GetJsonString(url);
                JsonObject myobj = JsonObject.Parse(jsonstr);
                JsonObject myres = myobj.GetNamedObject("result");
                bool status = myres.GetNamedBoolean("status");
                JsonArray data = myres.GetNamedArray("data");

                CategoryData catlist = new CategoryData();

                if (status)
                {
                    //get the list of categories
                    foreach (var item in data)
                    {
                        //get the list of key, value pairs in each Category
                        Dictionary<string, string> tempdict = new Dictionary<string, string>();
                        foreach (var details in item.GetObject())
                        {
                            tempdict.Add(details.Key, details.Value.Stringify());
                        }

                        CategoryItem tempitem = new CategoryItem();
                        bool res;
                        string resstr;
                        #region regionCategoryDetails
                        res = tempdict.TryGetValue("id", out resstr);
                        if (res) { tempitem.id = Convert.ToInt32(resstr); } resstr = null;

                        res = tempdict.TryGetValue("name", out resstr);
                        if (res) { tempitem.name = resstr.Trim(trimchars); } resstr = null;
                        #endregion
                        catlist.Items.Add(tempitem);
                    }
                }
                return catlist;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }

    public class CategoryItem
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}