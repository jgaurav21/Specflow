
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowMozart.Bases
{
    /// <summary>
    /// Products of Insight
    /// </summary>
    public enum Product
    {
        Leads,
        Analyze,
        Forecast,
        Pulse
    }

    public enum GridOptions
    {
        Project,
        Companies
    }

    public enum ManageSearchesGrids
    {
        Leads,
        Analyze,
        Forecast
    }
    
    public enum UserMenuOption
    {
        [Description("My Profile")]
        MyProfile,

        [Description("Manage Searches")]
        ManageSearches,
        ManageUsers,
        ManageTeam,
        Settings,
        Support,
        DocumentCenter,
        Help,
        ContactUs,
        Logout
    }

    public enum GridPaginationButton
    {
        Next,
        Last,
        Prev,
        First
    }
}
