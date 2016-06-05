using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;

namespace WpfApplication37
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ObservableCollection<DirectoryEntry> entries1 = new ObservableCollection<DirectoryEntry>();
        ObservableCollection<DirectoryEntry> subEntries1 = new ObservableCollection<DirectoryEntry>();

        ObservableCollection<DirectoryEntry> entries2 = new ObservableCollection<DirectoryEntry>();
        ObservableCollection<DirectoryEntry> subEntries2 = new ObservableCollection<DirectoryEntry>();
        string lastfolder1, lastfolder2;

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {

            DriveInfo[] allDrives = DriveInfo.GetDrives();


            foreach (string s in Directory.GetLogicalDrives())
            {
                DirectoryEntry d = new DirectoryEntry(s, s, "<Driver>", "<DIR>", Directory.GetLastWriteTime(s), "Images\\dir.gif", EntryType.Dir);
                foreach (DriveInfo driver in allDrives)
                {
                    if (driver.IsReady && driver.ToString() == d.Name)
                    {
                        entries1.Add(d);
                       
                    }
                }

            }

            this.listView1.DataContext = entries1;
            this.listView2.DataContext = entries1;
            
        }

        void listViewItem1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = e.Source as ListViewItem;

            DirectoryEntry entry = item.DataContext as DirectoryEntry;
            String path;
            if (entry.Name == "....")
            {
                if (entry.Fullpath == "")
                {
                    subEntries1.Clear();
                    DriveInfo[] allDrives = DriveInfo.GetDrives();


                    foreach (string s in Directory.GetLogicalDrives())
                    {
                        DirectoryEntry d = new DirectoryEntry(s, s, "<Driver>", "<DIR>", Directory.GetLastWriteTime(s), "Images/folder.gif", EntryType.Dir);
                        foreach (DriveInfo driver in allDrives)
                        {
                            if (driver.IsReady && driver.ToString() == d.Name)
                            {
                                subEntries1.Add(d);

                            }
                        }

                    }
                    listView1.DataContext = subEntries1;
                    return;
                }

            }

            if (entry.Type == EntryType.Dir)
            {
                string lastpath = "";
                if (Directory.GetParent(entry.Fullpath) != null)
                    lastpath = Directory.GetParent(entry.Fullpath+entry.Name).ToString();
                lastfolder1 = entry.Fullpath;
                System.Console.WriteLine(lastfolder1);


                subEntries1.Clear();
                //System.Console.WriteLine(entry.Lastpath);
                DirectoryEntry dd = new DirectoryEntry(
                        "....", lastpath, "<Folder>", "<DIR>",
                        entry.Date,
                        "Images/dir.gif", EntryType.Dir);
                subEntries1.Add(dd);          
                
               
                foreach (string s in Directory.GetDirectories(entry.Fullpath))
                {
                    DirectoryInfo dir = new DirectoryInfo(s);
                    DirectoryEntry d = new DirectoryEntry(
                        dir.Name, dir.FullName, "<Folder>", "<DIR>",
                        Directory.GetLastWriteTime(s),
                        "Images/dir.gif", EntryType.Dir);
                    d.Lastpath = entry.Fullpath;
                    subEntries1.Add(d);
                }
                foreach (string f in Directory.GetFiles(entry.Fullpath))
                {
                    FileInfo file = new FileInfo(f);
                    DirectoryEntry d = new DirectoryEntry(
                        file.Name, file.FullName, file.Extension, file.Length.ToString(),
                        file.LastWriteTime,
                        "Images/file.gif", EntryType.File);
                    subEntries1.Add(d);
                }

                listView1.DataContext = subEntries1;
            }
            if (entry.Type == EntryType.File)
            {
                System.Diagnostics.Process.Start(entry.Fullpath);
            }

        }

        void listViewItem2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = e.Source as ListViewItem;

            DirectoryEntry entry = item.DataContext as DirectoryEntry;
            String path;
            if (entry.Type == EntryType.File)
            {
                System.Diagnostics.Process.Start(entry.Fullpath);
            }

            if (entry.Name == "....")
            {
                if (entry.Fullpath == "")
                {
                    subEntries2.Clear();
                    DriveInfo[] allDrives = DriveInfo.GetDrives();


                    foreach (string s in Directory.GetLogicalDrives())
                    {
                        DirectoryEntry d = new DirectoryEntry(s, s, "<Driver>", "<DIR>", Directory.GetLastWriteTime(s), "Images/dir.gif", EntryType.Dir);
                        foreach (DriveInfo driver in allDrives)
                        {
                            if (driver.IsReady && driver.ToString() == d.Name)
                            {
                                subEntries2.Add(d);

                            }
                        }

                    }
                    listView2.DataContext = subEntries2;
                    return;
                }

            }

            if (entry.Type == EntryType.Dir)
            {
                string lastpath = "";
                if (Directory.GetParent(entry.Fullpath) != null)
                    lastpath = Directory.GetParent(entry.Fullpath).ToString();
                lastfolder2 = entry.Fullpath;

                subEntries2.Clear();
                //System.Console.WriteLine(entry.Lastpath);
                DirectoryEntry dd = new DirectoryEntry(
                        "....", lastpath, "<Folder>", "<DIR>",
                        entry.Date,
                        "Images/dir.gif", EntryType.Dir);
                subEntries2.Add(dd);


                foreach (string s in Directory.GetDirectories(entry.Fullpath))
                {
                    DirectoryInfo dir = new DirectoryInfo(s);
                    DirectoryEntry d = new DirectoryEntry(
                        dir.Name, dir.FullName, "<Folder>", "<DIR>",
                        Directory.GetLastWriteTime(s),
                        "Images/dir.gif", EntryType.Dir);
                    d.Lastpath = entry.Fullpath;
                    subEntries2.Add(d);
                }
                foreach (string f in Directory.GetFiles(entry.Fullpath))
                {
                    FileInfo file = new FileInfo(f);
                    DirectoryEntry d = new DirectoryEntry(
                        file.Name, file.FullName, file.Extension, file.Length.ToString(),
                        file.LastWriteTime,
                        "Images/file.gif", EntryType.File);
                    subEntries2.Add(d);
                }

                listView2.DataContext = subEntries2;
                
            }


        }

        private void ButtonNew1_Click(object sender, RoutedEventArgs e)
        {
           /* DirectoryEntry tmp_dir = listView1.Items[1] as DirectoryEntry;
            System.Console.WriteLine(tmp_dir.Lastpath);*/
            if (lastfolder1 != null)
            {
                NewFolder nf = new NewFolder();
                nf.label_path.Content = lastfolder1;
                //System.Console.WriteLine(listView1.SelectedValue);
                nf.ShowDialog();
            }
        }

        private void ButtonNew2_Click(object sender, RoutedEventArgs e)
        {
            DirectoryEntry tmp_dir = listView2.Items[1] as DirectoryEntry;
            System.Console.WriteLine(tmp_dir.Lastpath);
            if (lastfolder2 != null)
            {
                NewFolder nf = new NewFolder();
                nf.label_path.Content = lastfolder2;
                //System.Console.WriteLine(listView1.SelectedValue);
                nf.ShowDialog();
            }
        }

        private void Cp1_Button_Click(object sender, RoutedEventArgs e)
        {
           /* DirectoryEntry tmp1_dir = listView1.Items[1] as DirectoryEntry;
            DirectoryEntry tmp2_dir = listView2.Items[1] as DirectoryEntry;*/
            System.Console.WriteLine(lastfolder1);
            System.Console.WriteLine(lastfolder2);
            if (lastfolder1 != null && lastfolder2 != null && listView1.SelectedValue !=null)
            {
                //Copy Logic here
                masol(listView1.SelectedValue as DirectoryEntry,lastfolder2);
                
            }

        }
        private void Cp2_Button_Click(object sender, RoutedEventArgs e)
        {
           /* DirectoryEntry tmp1_dir = listView1.Items[1] as DirectoryEntry;
            DirectoryEntry tmp2_dir = listView2.Items[1] as DirectoryEntry;*/

          //  System.Console.WriteLine(tmp1_dir.Lastpath);
          //  System.Console.WriteLine(tmp2_dir.Fullpath);
            if (lastfolder1 != null && lastfolder2 != null && listView2.SelectedValue != null)
            {
                //Copy Logic here
                masol(listView2.SelectedValue as DirectoryEntry, lastfolder1);
                
            }

        }

        private delegate void dUpdateProgressBar(int pValue);

        private void UpdateProgressBar(int pValue)
        {
            this.progressBar1.Value = pValue;
        }

        public Stream celStream;
        public int prValue;

        public class MyAsyncInfo
        {
            public Byte[] ByteArray { get; set; }
            public Stream MyStream { get; set; }

            public MyAsyncInfo(Byte[] array, Stream stream)
            {
                ByteArray = array;
                MyStream = stream;
            }
        }


        private void masol(DirectoryEntry mit, string hova)
        {
            string celnev;

            


            FileStream stream = null;
            try
            {
                celnev = hova +"\\" + mit.Name;
                stream = File.OpenRead(mit.Fullpath);
                if (celnev == mit.Fullpath)
                    celStream = File.Create(celnev + ".new");
                else
                    celStream = File.Create(celnev);

                this.progressBar1.Maximum = (int)stream.Length;
                prValue = 0;
                this.progressBar1.Value = prValue;

            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Failed to open the file.");
                return;
            }

            Byte[] myByteArray = new Byte[1000];

            try
            {
                stream.BeginRead(myByteArray, 0, 1000,
                  new AsyncCallback(OnRead), new MyAsyncInfo(myByteArray, stream));
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Failed to copy.");
                stream.Close();
            }
        }

        private void OnRead(IAsyncResult ar)
        {
            MyAsyncInfo info = (MyAsyncInfo)ar.AsyncState;

            int amountRead = 0;
            try
            {
                amountRead = info.MyStream.EndRead(ar);
                prValue += amountRead;

            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Failed to read from file.");
                info.MyStream.Close();
                return;
            }

            string text = Encoding.UTF8.GetString(info.ByteArray, 0, amountRead);

            dUpdateProgressBar mUpdateProgressBar = new dUpdateProgressBar(UpdateProgressBar);
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, mUpdateProgressBar, prValue);

            celStream.Write(info.ByteArray, 0, amountRead);
            if (info.MyStream.Position < info.MyStream.Length)
            {
                try
                {

                    info.MyStream.BeginRead(info.ByteArray, 0,
                      1000, new AsyncCallback(OnRead), info);
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Failed to contine copying.");
                    info.MyStream.Close();
                }
            }
            else
            {
                info.MyStream.Close();
                celStream.Close();
                prValue = 0;
                System.Windows.MessageBox.Show("File copied.");
                this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, mUpdateProgressBar, prValue);
                
            }
        }
        
    }

    public enum EntryType
    {
        Dir,
        File
    }

    public class DirectoryEntry
    {
        private string _name;
        private string _fullpath;
        private string _lastpath;
        private string _ext;
        private string _size;
        private DateTime _date;
        private string _imagepath;
        private EntryType _type;

        public DirectoryEntry(string name,string fullname, string ext, string size, DateTime date, string imagepath, EntryType type)
        {
            _name = name;
            _fullpath = fullname;
            _ext = ext;
            _size = size;
            _date = date;
            _imagepath = imagepath;
            _type = type;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public string Ext
        {
            get { return _ext; }
            set { _ext = value; }
        }

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }
        
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        
        public string Imagepath
        {
            get { return _imagepath; }
            set { _imagepath = value; }
        }

        public EntryType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Fullpath
        {
            get { return _fullpath; }
            set { _fullpath = value; }
        }

        public string Lastpath
        {
            get { return _lastpath; }
            set { _lastpath = value; }
        }
    }
  }
