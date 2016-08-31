namespace Wangkanai.Browser
{
    public class Version
    {
        public Version(string major, string minor)
        {
            Major = major;
            Minor = minor;
        }
        public Version(string major, string minor, string patch, string build)
            :this(major, minor)
        {
            Patch = patch;
            Build = build;
        }

        public string Major { get;  }
        public string Minor { get;  }
        public string Patch { get;  }
        public string Build { get;  }

    }
}