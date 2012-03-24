using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using OSUEvents;


// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace OSU_Events_App.Data
{
    /// <summary>
    /// Base class for <see cref="SampleDataItem"/> and <see cref="SampleDataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class SampleDataCommon : OSU_Events_App.Common.BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public SampleDataCommon(String uniqueId, String title, String subtitle, String imagePath, String description)
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
                    this._image = new BitmapImage(new Uri(SampleDataCommon._baseUri, this._imagePath));
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

    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class SampleDataItem : SampleDataCommon
    {
        public SampleDataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content, SampleDataGroup group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this._content = content;
            this._group = group;
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private SampleDataGroup _group;
        public SampleDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleDataGroup : SampleDataCommon
    {
        public SampleDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
        }

        private ObservableCollection<SampleDataItem> _items = new ObservableCollection<SampleDataItem>();
        public ObservableCollection<SampleDataItem> Items
        {
            get { return this._items; }
        }
    }
    public class Professor
    {
        public String name;
        public String about;
        public String background;
        public String imagePath;
        public Professor(string name, string about, string background, string imagePath)
        {
            this.name = name;
            this.about = about;
            this.background = background;
            this.imagePath = imagePath;
            if (this.imagePath.Length == 0)
            {
                this.imagePath = "Assets/person.jpg";
            }
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// </summary>
    public sealed class SampleDataSource
    {
        private ObservableCollection<SampleDataGroup> _itemGroups = new ObservableCollection<SampleDataGroup>();
        public ObservableCollection<SampleDataGroup> ItemGroups
        {
            get { return this._itemGroups; }
        }
        public List<Professor> getFaculty()
        {
            List<Professor> list = new List<Professor>();
            list.Add(new Professor("GaganAgrawal", "Professor\nPh.D., University of Maryland, College Park\nOffice: 781, Extension: 688-8450, Email: {agrawal}\nInterests: Parallel and\ndistributed computing, data intensive computing, grid computing, compilers\n", "", "Assets/gagan.jpg"));
            list.Add(new Professor("Anish Arora", "Professor\nPh.D., University of Texas, Austin\nOffice: 587, Extension: 292-1836, Email: {anish}\nInterests: Fault-tolerance,\ndistributed systems, concurrency semantics\n", "I lead a research group on Dependable Distributed and Networked Systems.  The group works on the foundations of fault-tolerance, security, and timeliness properties,   develops design, verification, and implementation methods,   and builds prototypes of dependable systems for new application areas." +
            "We are especially interested in discovering new methods for scalable dependability,  as well as in characterizing the differences between reasoning about system correctness versus reasoning about systems dependability. Our methods draw from the theory of self-stabilization, and exploit formal specifications of and \"white box\" knowledge about the system. Demonstrations of our work are presently focused on embedded sensor network applications and internet services. " +
            "A notable recent accomplishment in wireless sensor networks is ExScal which demonstrated a perimeter security application with over 1000 sensor nodes and over 200 802.11b backbone nodes spread over a 1.3km by 300m area. As of December 2004, ExScal was the largest wireless sensor network deployed and likely also the largest 802.11b peer-to-peer ad hoc network deployed. The research led to the development of Kansei, which is an interactive, heterogeneous, large-scale wireless sensor network environment for testing and development. The stationary array of Kansei is housed in a warehouse near our Campus and presently contains over 700 sensor nodes supporting different hardware and software platforms. Other Kansei arrays are located in our department building, and supporting sensing for our in-building mobile peer-to-peer People Net, and being cloned nationwide as part of our effort to integrate Kansei with GENI experimentation infrastructure." +
            "In the last few years, we have collaborated actively with colleagues at UT at Austin, Iowa, Michigan State, Kent State, UC Berkeley, and MIT, as well as other university and industry partners (research resulting from our DARPA NEST project may be found in our Stabilization in NEST page) and with members of the Systems and Networking Group at Microsoft Research in Redmond, WA  (research resulting from the MSR Aladdin Home Networking Project may be found in the Publications page). In 2003, we completed a smart-dust sensor network field experiment on A Line in the Sand for DARPA and a demonstration of continuous self-maintenance at the Microsoft Research Faculty Summit. Our more recent projects are in collaboration with the Institute for Sensing Systems at Ohio State, which I helped co-found (alongwith Randy Moses).", "Assets/arora.gif"));

            list.Add(new Professor("Mikhail Belkin", "Associate Professor\nPh.D., University of Chicago\nOffice: 597, Extension: 292-5841, Email: {mbelkin}\nInterests: Machine learning,\nartificial intelligence\n", "Research interests: theory and applications of machine and human learning." +
            "My research focuses on designing and analyzing practical algorithms for machine learning based on non-linear structure of high dimensional data, in particular manifold and spectral methods. I am also interested in a range of theoretical questions concerning the computational and" +
            " statistical limits of learning and mathematical foundations of learning structure from data. Recently, I have also become interested in human cognition and its connections to machine learning.", "Assets/belkin.jpg"));

            list.Add(new Professor("Michael Bond", "Assistant Professor\n				 Ph.D., University of Texas, Austin\n				 Office: 697, Extension: 292-1408, Email: {mikebond}\n			\n			 Interests: Programming languages, runtime systems,\n				 dynamic analysis, compilers, security\n", "Hi, I'm Mike Bond, an assistant professor at Ohio State CSE. My research develops program analyses and software systems that make complex, concurrent software significantly more reliable, scalable, and secure than it is today. General interests: programming languages, software systems, runtime systems, program analysis, compilers, security.", "Assets/bond.jpeg"));

            list.Add(new Professor("Roger Crawfis", "Associate Professor\nPh.D., University of California, Davis\nOffice: 683, Extension: 292-2566, Email: {crawfis}\nInterests: Computer graphics\nand scientific visualization\n", "Welcome to my home page. I am an Associate Professor in the Computer Science and Engineering Department at The Ohio State University. I am also an Adjunct Professor for The Advanced Computing Center for the Arts and Design (ACCAD) and the Biomedical Engineering Department" +
            "I received my PhD in 1995 from the University of California, Davis/Livermore. I had previously been employed at Lawrence Livermore National Laboratory from 1984 until 1996, where I performed research on advanced techniques for scientific visualization and the Intelligent Archive Project. I recieved my B.S. in Computer Science and Applied Mathematics from Purdue University in 1984.", "Assets/crawfis.gif"));

            list.Add(new Professor("Jim Davis", "Associate Professor\nPh.D., Massachusetts Institute of Technology\nOffice: 491, Extension: 292-1553, Email: {jwdavis}\nInterests: Computer vision,\nartificial intelligence, cognitive science\n", "James W. Davis, Dept. of Computer Science and Engineering, is developing advanced video surveillance systems that use computers equipped with video cameras to not only detect the presence of people and track them, but also to identify their activities. The research has broad implications for Homeland Security as well as search and rescue, border patrol, law enforcement and many other types of military applications. The systems combine video cameras with machine learning methods, enabling the computer to perform the kind of visual recognition that seems effortless for humans. Davis' work in investigating computer vision methods was recognized by the National Science Foundation with the prestigious NSF Faculty Early Career Development (CAREER) Program award. Support for this research (past and present) has been provided by the National Science Foundation, U.S. Air Force, Los Alamos National Lab, U.S. Army Night Vision Laboratory, Intel, and Ohio Board of Regents.", "Assets/davis.jpg"));
            list.Add(new Professor("Tamal Dey", "Professor\nPh.D., Purdue University\nOffice: 483, Extension: 292-3563, Email: {tamaldey}\nInterests: Computational\ngeometry and computer graphics\n", "", ""));
            list.Add(new Professor("Brian Kulis", "Assistant Professor\n				 Ph.D., University of Texas at Austin\n				 Email: {kulis}\n			\n			 Interests: Machine learning\n		\n", "", ""));
            list.Add(new Professor("Eric Fosler-Lussier", "Associate Professor\nPh.D., University of California, Berkeley\nOffice: 585, Extension: 292-4890, Email: {fosler}\nInterests: Spoken language\nprocessing, automatic speech recognition, computational lingustics,\nartificial intelligence\n", "", ""));
            list.Add(new Professor("Ten-Hwang Steve Lai", "Professor\nPh.D., University of Minnesota\nOffice: 581, Extension: 292-2146, Email: {lai}\nInterests: Networking,\nparallel and distributed computing\n", "", ""));

            list.Add(new Professor("David Lee", "Ohio Board of Regents Distinguished Professor\nPh.D., Columbia University\nOffice: 681, Extension: 688-3502, Email: {lee}\nInterests: Networking\n", "David Lee was born in Hong Kong. He received a master's degree in Mathematics from Hunter College of City University of New York in 1982. He obtained both a master's and a Ph.D. degree in Computer Science from Columbia University in 1985." +
            "He joined Bell Laboratories Research at Murray Hill in 1985 and became director of Networking Research Department in 1998. He was promoted to Vice President of Bell Labs Research in 1999. He took an International Assignment in Beijing, China, from 2000 to 2004, and has founded Bell Labs Research China and Asia Pacific. In 2004, he joined the Department of Computer Science and Engineering, The Ohio State University, as Ohio Board of Regents Distinguished Professor, Chair of the Consortium of Ohio State University Center of Information Assurance and Security Education and Research, designated by NSA/DHS, and Director of Networking Research Laboratories." +
            "His current research interests include: information and communication system security and management, network monitoring and measurement, communication protocol system reliability, integration, interface and interoperability, and Instant Messaging. He has six US patents and more than 100 technical publications in journals and referenced conferences.", "Assets/david.jpg"));

            list.Add(new Professor("Raghu Machiraju", "Associate Professor\nPh.D., The Ohio State University\nOffice: 779, Extension: 292-6730, Email: {raghu}\nInterests: Computer graphics\n", "", ""));
            list.Add(new Professor("Arnab Nandi", "Assistant Professor\n				 Ph.D., University of Michigan, Ann Arbor\n				 Email: {arnab}\n			\n			 Interests:Databases, large-scale\n				 analytics, data interaction\n", "", ""));
            list.Add(new Professor("D.K. Panda", "Professor\nPh.D., University of Southern California\nOffice: 785, Extension: 292-5199, Email: {panda}\nInterests: Network-based\ncomputing, parallel architecture\n", "Dr. Dhabaleswar K. (DK) Panda is a Professor of Computer Science at the Ohio State University. He obtained his Ph.D. in computer engineering from the University of Southern California. His research interests include parallel computer architecture, high performance networking, InfiniBand, network-based computing, exascale computing, programming models, GPUs and accelerators, high performance file systems and storage, virtualization and cloud computing. He has published over 300 papers in major journals and international conferences related to these research areas. Dr. Panda has served as Program Chair/Co-Chair/Vice Chair of many international conferences and workshops including HiPC '11, IEEE Cluster (Cluster)'10, Supercomputing (SC)'08, ANCS '07, Hot Interconnect 2007, IPDPS '07, HiPC '07, Hot Interconnect 2006, CAC (2001-04), ICPP '01, CANPC (1997-98) and ICPP '98. Currently, he is serving as the Program Chair for HiPC '12 and a Vice Chair for CCGrid '12. He has served as the General Chair of ICPP '06. He has served as an Associate Editor of IEEE TPDS in past. Currently, he is serving as an Associated Editor of IEEE Transactions on Computers (IEEE TC) and Journal of Parallel and Distributed Computing (JPDC). He has served as Program Committee Member for more than 95 international conferences and workshops. Prof. Panda is a motivated speaker. He has Served as an IEEE Distinguished Visitor and an IEEE Chapters Tutorial Speaker. He has delivered a large number of invited Keynote/Plenary Talks, Tutorials and Presentations Worldwide." +
            "Dr. Panda and his research group members have been doing extensive research on modern networking technologies including InfiniBand and 10GE/iWARP. His research group is currently collaborating with National Laboratories and leading InfiniBand and 10GE/iWARP companies on designing various subsystems of next generation high-end systems. The MVAPICH/MVAPICH2 (High Performance MPI over InfiniBand and iWARP) open-source software project , developed by his research group, are currently being used by more than 1,850 organizations worldwide (in 66 countries). This software has enabled several InfiniBand clusters to get into the latest TOP500 ranking. More than 100,000 downloads of this software have taken place from the project website alone. These software packages are also available with the Open Fabrics stack for network vendors (InfiniBand and iWARP), server vendors and Linux distributors (such as RedHat and SuSE)." +
            "Dr. Panda leads Network-Based Computing Research Group . Students and staff members of this group are involved in multiple state-of-the-art research projects . Members of his group have obtained a large number of Awards and Recognitions . Dr. Panda's research is supported by funding from US National Science Foundation, US Department of Energy, Ohio Board of Regents and several industry including IBM, Intel, Cisco, SUN, Mellanox, NVIDIA, QLogic and NetApp." +
            "Dr. Panda is a Fellow of IEEE and a member of ACM.", "Assets/Panda.jpg"));
            list.Add(new Professor("Richard Parent", "Professor\nPh.D., The Ohio State University\nOffice: 787, Extension: 292-0055, Email: {parent}\nInterests: Computer graphics\nand animation\n", "", ""));
            list.Add(new Professor("Srinivasan Parthasarathy", "Professor\nPh.D., University of Rochester\nOffice: 693, Extension: 292-2568, Email: {srini}\nInterests: Data Mining,\nparallel and distributed systems\n", "", ""));
            list.Add(new Professor("Feng Qin", "Assistant Professor\nPh.D., University of Illinois at Urbana-Champaign\nOffice: 699, Extension: 247-4533, Email: {qin}\nInterests: Software Dependability,\nSecurity, and Operating Systems\n", "I am an assistant professor in the Department of Computer Science and Engineering since September 2006. Before that, I was a graduate student in Yuanyuan Zhou's OPERA group at University of Illinois at Urbana-Champaign. My research interests include Software Dependability, Operating Systems, and Security." +
"For prospective students: If you are interested in solving cutting-edge problems in Software Dependability, Security, and Operating Systems, come and talk to me, or drop me a message.", "Assets/fengqin.jpg"));
            list.Add(new Professor("Atanas Rountev", "Associate Professor\nPh.D., Rutgers University\nOffice: 685, Extension: 292-7203, Email: {rountev}\nInterests: Software engineering,\nprogramming languages, tools for software analysis and testing\n", "", ""));
            list.Add(new Professor("Luis Rademacher", "Assistant Professor\n			    Ph.D., Massachusetts Institute of Technology\n			    Office: 495, Extension: 292-3083, Email: {lrademac}\n		\n		    Interests: Theoretical computer science,\n			    computational geometery\n	\n", "", ""));
            list.Add(new Professor("P. Sadayappan", "Professor\nPh.D., State University of New York, Stony Brook\nOffice: 595, Extension: 292-0053, Email: {saday}\nInterests: Parallel algorithms\nand computation\n", "", ""));
            list.Add(new Professor("Han-Wei Shen", "Associate Professor\nPh.D., University of Utah\nOffice: 789, Extension: 292-0060, Email: {hwshen}\nInterests: Computer graphics\n", "", ""));
            list.Add(new Professor("Ness Shroff", "Ohio Eminent Scholar in Networking and Communications, and Professor of CSE and ECE\n	  Ph.D., Columbia University\nOffice: 764, Extension: 247-6554, Email: {shroff}\nInterests: Wireless and wireline communication networks, network security, sensor networks.\n", "", ""));
            list.Add(new Professor("Prasun Sinha", "Assistant Professor\nPh.D., University of Illinois, Urbana-Champaign\nOffice: 791, Extension: 292-1531, Email: {prasun}\nInterests: Wireless networks,\nad hoc networks and sensor networks\n", "", ""));
            list.Add(new Professor("Paul Sivilotti", "Associate Professor\nPh.D., California Institute of Technology\nOffice: 695, Extension: 292-5835, Email: {paolo}\nInterests: Distributed\nsystems and software engineering\n", "", ""));
            list.Add(new Professor("Neelam Soundarajan", "Associate Professor\nPh.D., Bombay University, India, 1978\nOffice: 579, Extension: 292-1444, Email: {neelam}\nInterests: Semantics of\ndistributed, concurrent programs\n", "", ""));
            list.Add(new Professor("Kannan Srinivasan", "Assistant Professor\n				 Ph.D., Stanford University\n				 Office: 681, Extension: 688-3502, Email: {kannan}\n			\n			 Interests:Wireless\n				 networks, communications systems and security.\n", "", ""));
            list.Add(new Professor("Christopher Stewart", "Assistant Professor\n			    Ph.D., University of Rochester\n			    Office: 793, Extension: 292-7325, Email: {cstewart}\n		\n		    Interests: Operating systems\n			    distributed systems, performance management and power management\n		\n	\n", "", ""));
            list.Add(new Professor("Kenneth Supowit", "Associate Professor\nPh.D., University of Illinois, Urbana-Champaign\nOffice: 679, Extension: 292-4895, Email: {supowit}\nInterests: Combinatorial\nalgorithms\n", "", ""));
            list.Add(new Professor("Radu Teodorescu", "Assistant Professor\nPh.D., University of Illinois, Urbana-Champaign\nOffice: 783, Extension: 292-7027, Email: {teodores}\nInterests:  Computer\nArchitecture with emphasis on hardware and  software reliability,\nnanoscale technology and power management.\n", "", ""));
            list.Add(new Professor("DeLiang Wang", "Professor\nPh.D., University of Southern California\nOffice: 598, Extension: 292-6827, Email: {dwang}\nInterests: Neural networks\nand cognitive modeling\n", "", ""));
            list.Add(new Professor("Yusu Wang", "Associate Professor\nPh.D., Duke University\nOffice: 487, Extension: 292-1309, Email: {yusu}\nInterests: Computational\ngeometry, computer graphics\n", "", ""));
            list.Add(new Professor("Huamin Wang", "Assistant Professor\n				 Ph.D., Georgia Institute of Technology\n				 Office: 583, Extension: 292-6370, Email: {whmin}\n			\n			 Interests:Computer graphics and animation\n", "", ""));
            list.Add(new Professor("Bruce Weide", "Professor and Associate Department Chair\nPh.D., Carnegie Mellon University\nOffice: 687, Extension: 292-1517, Email: {weide}\nInterests: Component-based\nsoftware\n", "", ""));
            list.Add(new Professor("Rephael Wenger", "Associate Professor\nPh.D., McGill University\nOffice: 485, Extension: 292-6253, Email: {wenger}\nInterests: Computer graphics\nand computational geometry\n", "", ""));
            list.Add(new Professor("Dong Xuan", "Associate Professor\nPh.D., Texas A&amp;M University\nOffice: 593, Extension: 292-2958, Email: {xuan}\nInterests: Real-time computing\nand communication, network security, distributed systems\n", "", ""));
            list.Add(new Professor("Xiaodong Zhang", "Robert M. Critchfield Professor and Department Chair\nPh.D., University of Colorado at Boulder\nOffice: 479 Extension: 292-2770, Email: {zhang}\nInterests: Distributed\nand Computer Systems\n", "Xiaodong Zhang is the Robert M. Critchfield Professor in Engineering, and Chairman of Department of Computer Science and Engineering at the Ohio State University." +
            "In the High Performance Computing and Software Laboratory, he has supervised over 50 graduate students (both MS and Ph.D), post-docs, and visiting scholars. His research interests cover a wide spectrum in the areas of high performance and distributed systems. A common thread among his research projects focuses on fast data accesses and resource sharing with cost- and energy-efficient management at different levels of the memory and storage hierarchies in computer, distributed, and Internet systems. He has made strong efforts to transfer his academic research into advanced technology to impact general-purpose production systems in both hardware and software. Several technical innovations and research results from his team have been widely adopted in commercial processors, major operating systems and databases with direct contributions to the advancement of the computer and distributed systems. This list of selected and representative papers reflects his long term research efforts." +
            "Besides being actively involved in various professional activities, Xiaodong Zhang has been an organizer and a lecturer of the Dragon Star Lecture Program, a volunteer-based teaching program offering advanced research classes of computer science and engineering in many Chinese universities for thousands of talented graduate students and young researchers every year." +
            "Xiaodong Zhang received and his B.S. in Electrical Engineering from Beijing University of Technology, and his Ph.D. in Computer Science from the University of Colorado at Boulder, where he is a recipient of the Distinguished Engineering Alumni Award. He is a Fellow of the IEEE." +
            "Xiaodong Zhang spent his first two years in the U.S. working with Dr. Ralph Slutz (1917-2005), who was a world-class scholar and a computer pioneer.", "Assets/zhang.jpg"));
            return list;
        }
        public SampleDataSource()
        {
            
            String ITEM_CONTENT = String.Format("Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                        "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");
            
            var group1 = new SampleDataGroup("Faculty",
                    "Faculty ",
                    "",
                    "Assets/faculty.jpg",
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            EventsDataSource eventDataSource = new EventsDataSource();
            
            foreach (Professor prof in this.getFaculty())
            {
                group1.Items.Add(new SampleDataItem(prof.name,
                    prof.name,
                    "",
                    prof.imagePath,
                    prof.about,
                    prof.background,
                    group1));

            }
            this.ItemGroups.Add(group1);
            
            var group2 = new SampleDataGroup("Group-2",
                    "Group Title: 2",
                    "Group Subtitle: 2",
                    "Assets/LightGray.png",
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group2.Items.Add(new SampleDataItem("Group-2-Item-1",
                    "Item Title: 1",
                    "Item Subtitle: 1",
                    "Assets/DarkGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group2));
            group2.Items.Add(new SampleDataItem("Group-2-Item-2",
                    "Item Title: 2",
                    "Item Subtitle: 2",
                    "Assets/MediumGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group2));
            group2.Items.Add(new SampleDataItem("Group-2-Item-3",
                    "Item Title: 3",
                    "Item Subtitle: 3",
                    "Assets/LightGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group2));
            this.ItemGroups.Add(group2);

            var group3 = new SampleDataGroup("Group-3",
                    "Group Title: 3",
                    "Group Subtitle: 3",
                    "Assets/MediumGray.png",
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group3.Items.Add(new SampleDataItem("Group-3-Item-1",
                    "Item Title: 1",
                    "Item Subtitle: 1",
                    "Assets/MediumGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group3));
            group3.Items.Add(new SampleDataItem("Group-3-Item-2",
                    "Item Title: 2",
                    "Item Subtitle: 2",
                    "Assets/LightGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group3));
            group3.Items.Add(new SampleDataItem("Group-3-Item-3",
                    "Item Title: 3",
                    "Item Subtitle: 3",
                    "Assets/DarkGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group3));
            group3.Items.Add(new SampleDataItem("Group-3-Item-4",
                    "Item Title: 4",
                    "Item Subtitle: 4",
                    "Assets/LightGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group3));
            group3.Items.Add(new SampleDataItem("Group-3-Item-5",
                    "Item Title: 5",
                    "Item Subtitle: 5",
                    "Assets/MediumGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group3));
            group3.Items.Add(new SampleDataItem("Group-3-Item-6",
                    "Item Title: 6",
                    "Item Subtitle: 6",
                    "Assets/DarkGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group3));
            group3.Items.Add(new SampleDataItem("Group-3-Item-7",
                    "Item Title: 7",
                    "Item Subtitle: 7",
                    "Assets/MediumGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group3));
            this.ItemGroups.Add(group3);

            var group4 = new SampleDataGroup("Group-4",
                    "Group Title: 4",
                    "Group Subtitle: 4",
                    "Assets/LightGray.png",
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group4.Items.Add(new SampleDataItem("Group-4-Item-1",
                    "Item Title: 1",
                    "Item Subtitle: 1",
                    "Assets/DarkGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group4));
            group4.Items.Add(new SampleDataItem("Group-4-Item-2",
                    "Item Title: 2",
                    "Item Subtitle: 2",
                    "Assets/LightGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group4));
            group4.Items.Add(new SampleDataItem("Group-4-Item-3",
                    "Item Title: 3",
                    "Item Subtitle: 3",
                    "Assets/DarkGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group4));
            group4.Items.Add(new SampleDataItem("Group-4-Item-4",
                    "Item Title: 4",
                    "Item Subtitle: 4",
                    "Assets/LightGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group4));
            group4.Items.Add(new SampleDataItem("Group-4-Item-5",
                    "Item Title: 5",
                    "Item Subtitle: 5",
                    "Assets/MediumGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group4));
            group4.Items.Add(new SampleDataItem("Group-4-Item-6",
                    "Item Title: 6",
                    "Item Subtitle: 6",
                    "Assets/LightGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group4));
            this.ItemGroups.Add(group4);

            var group5 = new SampleDataGroup("Group-5",
                    "Group Title: 5",
                    "Group Subtitle: 5",
                    "Assets/MediumGray.png",
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group5.Items.Add(new SampleDataItem("Group-5-Item-1",
                    "Item Title: 1",
                    "Item Subtitle: 1",
                    "Assets/LightGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group5));
            group5.Items.Add(new SampleDataItem("Group-5-Item-2",
                    "Item Title: 2",
                    "Item Subtitle: 2",
                    "Assets/DarkGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group5));
            group5.Items.Add(new SampleDataItem("Group-5-Item-3",
                    "Item Title: 3",
                    "Item Subtitle: 3",
                    "Assets/LightGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group5));
            group5.Items.Add(new SampleDataItem("Group-5-Item-4",
                    "Item Title: 4",
                    "Item Subtitle: 4",
                    "Assets/MediumGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group5));
            this.ItemGroups.Add(group5);

            var group6 = new SampleDataGroup("Group-6",
                    "Group Title: 6",
                    "Group Subtitle: 6",
                    "Assets/DarkGray.png",
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group6.Items.Add(new SampleDataItem("Group-6-Item-1",
                    "Item Title: 1",
                    "Item Subtitle: 1",
                    "Assets/LightGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group6));
            group6.Items.Add(new SampleDataItem("Group-6-Item-2",
                    "Item Title: 2",
                    "Item Subtitle: 2",
                    "Assets/DarkGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group6));
            group6.Items.Add(new SampleDataItem("Group-6-Item-3",
                    "Item Title: 3",
                    "Item Subtitle: 3",
                    "Assets/MediumGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group6));
            group6.Items.Add(new SampleDataItem("Group-6-Item-4",
                    "Item Title: 4",
                    "Item Subtitle: 4",
                    "Assets/DarkGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group6));
            group6.Items.Add(new SampleDataItem("Group-6-Item-5",
                    "Item Title: 5",
                    "Item Subtitle: 5",
                    "Assets/LightGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group6));
            group6.Items.Add(new SampleDataItem("Group-6-Item-6",
                    "Item Title: 6",
                    "Item Subtitle: 6",
                    "Assets/MediumGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group6));
            group6.Items.Add(new SampleDataItem("Group-6-Item-7",
                    "Item Title: 7",
                    "Item Subtitle: 7",
                    "Assets/DarkGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group6));
            group6.Items.Add(new SampleDataItem("Group-6-Item-8",
                    "Item Title: 8",
                    "Item Subtitle: 8",
                    "Assets/LightGray.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group6));
            this.ItemGroups.Add(group6);
        }
    }
}
