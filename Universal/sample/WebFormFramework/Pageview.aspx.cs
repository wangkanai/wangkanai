using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wangkanai.Universal;

namespace WebFormFramework
{
    public partial class Pageview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var session = new Session();
            session.Pageview = new Wangkanai.Universal.Pageview("/home/pageview", "real-time visitor");
            (Master as SiteMaster).AnalyticsSession = session;
        }
    }
}