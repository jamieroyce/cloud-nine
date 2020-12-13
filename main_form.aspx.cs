using CSharpDemoWebsite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class main_form : System.Web.UI.Page
{
    private int _stateID;

    protected void Page_Load(object sender, EventArgs e)
    {
        //  The main form design sets the control states / properties, but
        //  call the load states dropdown method when the page loads . . .

        //  On first load of form AKA application launch, call
        //  the method to fill the states dropdown . . .

        if (!IsPostBack)
        {
            this.fillStatesDDLDS();
        }
    }

    protected void statesDropDownList_TextChanged(object sender, EventArgs e)
    {
        //  Set the class stateID property based on the selected
        //  states dropdown value, set form control properties
        //  as appropriate, and call the load counties dropdown
        //  method . . .

        this._stateID = Convert.ToInt32(this.statesDropDownList.SelectedValue);
        this.countiesDropDownList.Enabled = true;
        this.statesDropDownList.Enabled = false;
        this.btnBack.Enabled = true;
        this.loadCountiesDDL();
    }

    protected void countiesDropDownList_TextChanged(object sender, EventArgs e)
    {
        //   Declare a child data access object . . .

        childDBObject listCitiesOfCountiesDT = new childDBObject();

        //   Set up a list of SQL parameters for
        //   the relevant stored procedure . . .

        List<SqlParameter> prmList = new List<SqlParameter>();

        //  The user might choose the first value in the counties
        //  dropdown - PICK A COUNTY - so simply ignore that
        //  pick . . .

        if (this.countiesDropDownList.SelectedValue != "")
        {
            prmList.Add(new SqlParameter("@COUNTY_ID", Convert.ToInt32(this.countiesDropDownList.SelectedValue)));

            //   Set the stored procedure for the data access object . . .

            listCitiesOfCountiesDT.SPName = "SELECT_CITIES_OF_COUNTY";

            //   Set the data access object dateTimeStamp and SPPrms properties . . .

            listCitiesOfCountiesDT.dateTimeStamp = DateTime.Now;
            listCitiesOfCountiesDT.SPPrms = prmList;

            //   The childDBObject.ExecuteSP method overrides
            //   method DBObject.ExecuteSP . . .

            this.citiesOfCountyDG.DataSource = listCitiesOfCountiesDT.executeSP();
            this.citiesOfCountyDG.DataBind();

            this.statesDropDownList.Enabled = false;
            this.countiesDropDownList.Enabled = false;
            this.btnBack.Enabled = false;
            this.citiesOfCountyDG.Visible = true;
        }
    }

    private void fillStatesDDLDS()
    {
        //   Dim a new database access object, and set
        //   the stored procedure property. This SP
        //   doesn't need parameters, so don't worry
        //   about the SPParms property . . .

        parentDBObject listStatesDT = new parentDBObject();
        listStatesDT.SPName = "SELECT_ALL_STATES";

        //   Call the executeSP object method
        //   and use this as the combobox
        //   data source . . .

        this.statesDropDownList.DataSource = listStatesDT.executeSP();
        this.statesDropDownList.DataTextField = "STATE_NAME";
        this.statesDropDownList.DataValueField = "STATE_ID";
        this.statesDropDownList.DataBind();

        //  Artificially place a dummy dropdown list item so that the
        //  first result set pick in that list will work . . .

        this.statesDropDownList.Items.Insert(0, new ListItem("PICK A STATE", ""));
    }

    private void loadCountiesDDL()
    {
        //  Remove the dummy dropdown list item . . .

        this.statesDropDownList.Items.Remove(new ListItem("PICK A STATE", ""));
        parentDBObject listCountiesOfStateDT = new parentDBObject();
        listCountiesOfStateDT.SPName = "SELECT_COUNTIES_OF_STATE";

        //   Set up a list of SQL parameters for
        //   relevant the stored procedure . . .

        List<SqlParameter> prmList = new List<SqlParameter>();

        prmList.Add(new SqlParameter("@STATE_ID", _stateID));
        listCountiesOfStateDT.SPPrms = prmList;

        //   Call the executeSP object method
        //   and use this as the combobox
        //   data source . . .

        this.countiesDropDownList.DataSource = listCountiesOfStateDT.executeSP();
        this.countiesDropDownList.DataTextField = "COUNTY NAME";
        this.countiesDropDownList.DataValueField = "COUNTY_ID";
        this.countiesDropDownList.DataBind();

        //  Artificially place a dummy dropdown list item so that the
        //  first result set pick in that list will work . . .

        this.countiesDropDownList.Items.Insert(0, new ListItem("PICK A COUNTY", ""));
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //  Set control properties / states appropriately, clear
        //  the counties dropdown, and clear / refill the states
        //  dropdown . . .

        this.btnBack.Enabled = false;
        this.countiesDropDownList.Items.Clear();
        this.countiesDropDownList.Enabled = false;
        this.statesDropDownList.Enabled = true;
        this.fillStatesDDLDS();
    }

    protected void citiesOfCountyDG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //  Hide the state ID and city ID columns . . .

        if ((e.Row.RowType == DataControlRowType.DataRow) || (e.Row.RowType == DataControlRowType.Header))
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
        }
    }

    protected void citiesOfCountyDG_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //  After the user clicks the datagrid command button to return
        //  to the counties dropdown, set focus to that dropdown, enable
        //  that dropdown, select PICK A COUNTY in that dropdown, hide
        //  the datagrid, and enable the back button . . .

        this.countiesDropDownList.Focus();
        this.countiesDropDownList.Enabled = true;
        this.countiesDropDownList.SelectedValue = "";
        this.citiesOfCountyDG.Visible = false;
        this.btnBack.Enabled = true;
    }
}