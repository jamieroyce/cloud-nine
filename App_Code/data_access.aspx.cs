using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;

public class dal
{
    public DataTable reg(string head, string org = null)
    {
        if (head == "all")
            return Data_Load(String.Format("SELECT * from reg order by status, name"), "reg");
        else if (head == "Archive")
            if (org == "Combined")
            {
                return Data_Load(String.Format("SELECT * from reg where reg_cat_id = 'Archive' order by tm desc"), "reg");
            }
            else
            {
                return Data_Load(String.Format("SELECT * from reg where reg_cat_id = 'Archive' and org = '{0}' order by tm desc ", org), "reg");
            }
        else if (head == "config")
            return Data_Load(String.Format("SELECT * from listReg Order by short_name"), "reg");
        else if (org == "Combined")
            return Data_Load(String.Format("SELECT * from reg where status = '{0}' and reg_cat_id <> 'Archive'", head), "reg");
        else
            //return Data_Load(String.Format("SELECT * from reg where reg_cat_id = '{0}' and org = '{1}' order by rank, name", head, org), "reg");        
            return Data_Load(String.Format("SELECT * from reg where status = '{0}' and org = '{1}' and reg_cat_id <> 'Archive'", head, org), "reg");
    }

    public DataTable listReg(string org)
    {
        return Data_Load(String.Format("SELECT * from listReg where post = '{0}' ", org), "reg");
    }

    public DataTable getRegList(string org)
    {
        if (org == "Combined")
            return Data_Load(String.Format("SELECT * from listReg Order by short_name"), "reg");
        else
            return Data_Load(String.Format("SELECT * from listReg where post = '{0}' order by short_name ", org), "reg");
    }

    public DataTable getExpectancy(string org)
    {
        return Data_Load(String.Format("SELECT * from lookup where type = 'expectancy' and desc3 = '{0}' order by id ", org), "reg");
    }

    public DataTable getEvents(string untildate = null)
    {
        if (untildate != null)
            return Data_Load(String.Format("SELECT * from event where active = 'Y' and event_date <= '{0}' Order by event_date ", untildate), "reg");
        else
            return Data_Load(String.Format("SELECT * from event where active = 'Y' Order by event_date"), "reg");
    }

    public DataTable getArchivedEvents()
    {
        return Data_Load(String.Format("SELECT * from event where active = 'N' Order by event_date"), "reg");
    }

    public DataTable getEvent(string evt)
    {
        return Data_Load(String.Format("SELECT * from event where event_desc = '{0}' ", evt), "reg");
    }

    public DataTable reg_log(string head, string org = null)
    {
        if (head == "all")
            return Data_Load(String.Format("SELECT * from reg order by status, name"), "reg");
        else if (org == "Combined")
            return Data_Load(String.Format("SELECT * from reg where reg_cat_id <> 'Archive' order by status, name", head), "reg");
        else
            return Data_Load(String.Format("SELECT * from reg where org = '{0}' and reg_cat_id <> 'Archive' order by status, name", org), "reg");
    }

    public DataTable bis_log(string head, string org = null)
    {
        if (head == "all")
            return Data_Load(String.Format("SELECT * from bis where reg_cat_id = 'LineUp' order by status, name"), "reg");
        else if (org == "Combined")
            return Data_Load(String.Format("SELECT * from bis where reg_cat_id = 'LineUp' ", head), "reg");
        else
            return Data_Load(String.Format("SELECT * from bis where org = '{0}' and reg_cat_id = 'LineUp' ", org), "reg");
    }

    public DataTable get_log(string category, string org)
    {
        if (org == "Combined")
            return Data_Load(String.Format("SELECT * from bis where reg_cat_id = '{0}' ", category), "reg");
        else
            return Data_Load(String.Format("SELECT * from bis where org = '{0}' and reg_cat_id = '{1}' ", org, category), "reg");
    }

    public DataTable get_bis_data(string category, string org, string status)
    {
        if (org == "Combined")
            return Data_Load(String.Format("SELECT * from bis where reg_cat_id = '{0}' and status = '{1}'  ", category, status), "reg");
        else
            return Data_Load(String.Format("SELECT * from bis where org = '{0}' and reg_cat_id = '{1}' and status = '{2}' ", org, category, status), "reg");
    }

    public DataTable get_event_archive(string org, string evt, string type = null)
    {

        if (type == "all")
        {

            if (org == "Combined")
                return Data_Load(String.Format("SELECT * from bis where reg_cat_id = 'Archive' and status in ('Confirmed', 'Reconfirmed', 'Attended', 'Maybe', 'Not Coming', 'Prospect') ", evt), "reg");
            else
                return Data_Load(String.Format("SELECT * from bis where reg_cat_id = 'Archive' and status in ('Confirmed', 'Reconfirmed', 'Attended', 'Maybe', 'Not Coming', 'Prospect') and org = '{0}' ", org, evt), "reg");

        }
        else
        {

            if (org == "Combined")
                return Data_Load(String.Format("SELECT * from bis where reg_cat_id = 'Archive' and service like '%{0}%' ", evt), "reg");
            else
                return Data_Load(String.Format("SELECT * from bis where reg_cat_id = 'Archive' and service like '%{0}%' and org = '{1}' ", evt, org), "reg");

        }

    }

    public DataTable bis(string head, string org = null, string status = null)
    {
        if (head == "all")
            return Data_Load(String.Format("SELECT * from bis where reg_cat_id = 'LineUp'"), "reg");
        else if (head == "Archive")
            return Data_Load(String.Format("SELECT * from bis where reg_cat_id = 'Archive' and status = '{0}' ", status), "reg");
        else if (head == "config")
            return Data_Load(String.Format("SELECT * from lookup where type = 'bis' Order by desc1"), "reg");
        else if (org == "Combined")
            return Data_Load(String.Format("SELECT * from bis where status = '{0}' and reg_cat_id <> 'Archive'", head), "reg");
        else
            return Data_Load(String.Format("SELECT * from bis where status = '{0}' and org = '{1}' and reg_cat_id <> 'Archive'", head, org), "reg");
    }

    public DataTable fss(string head, string org = null, string status = null)
    {
        return Data_Load(String.Format("SELECT * from lookup where type = 'fss' Order by desc1"), "reg");
    }

    public DataTable lookup(string type)
    {
        return Data_Load(String.Format("SELECT * from lookup where type = '{0}' Order by desc1", type), "reg");
    }

    public DataTable getRegCat(string category)
    {
        return Data_Load(String.Format("SELECT * from bis where reg_cat_id = '{0}'", category), "reg");
    }

    public DataTable get_bis(string status, string addoid)
    {
        return Data_Load(String.Format("SELECT * from bis where status = '{0}' and addo_id = '{1}' and reg_cat_id <> 'Archive'", status, addoid), "reg");
    }

    public DataTable ReportBISArea(string weekend)
    {
        if (weekend == "")
            return Data_Load("select org, area, count(1) as cnt from bis where reg_cat_id = 'LineUp' and status = 'In The Shop' group by org, area order by org, area", "reg");
        else
            return Data_Load(String.Format("select org, area, count(1) as cnt from bis where reg_cat_id = 'Archive' and status = 'In The Shop' and weekend = '{0}' group by org, area order by org, area ", weekend), "reg");
    }

    public DataTable ReportAreaStatus(string category, string status)
    {
        return Data_Load(String.Format("select org, area, count(1) as cnt from bis where reg_cat_id = '{0}' and status = '{1}' group by org, area order by org, area", category, status), "reg");
    }

    public DataTable ReportLineCount(string category, string status)
    {
        return Data_Load(String.Format("select org, area, count(1) as cnt from reg where reg_cat_id = '{0}' and status = '{1}' group by org, area order by org, area", category, status), "reg");
    }

    public DataTable ReportByArea(string type)
    {
        if (type == "In and Started")
            return Data_Load("select org, area, count(1) as cnt from bis where reg_cat_id = 'In and Started' and status = 'In and Started' group by org, area order by org, area", "reg");
        else if (type == "Comp Resign")
            return Data_Load("select org, area, count(1) as cnt from bis where reg_cat_id = 'Comp Resign' and status = 'Comp Resign' group by org, area order by org, area", "reg");
        else if (type == "First Service")
            return Data_Load("select org, area, count(1) as cnt from bis where reg_cat_id = 'First Service' and status = 'First Service' group by org, area order by org, area", "reg");
        else if (type == "Purif Start")
            return Data_Load("select org, area, count(1) as cnt from bis where reg_cat_id = 'Purif' and status = 'Purif Start' group by org, area order by org, area", "reg");
        else if (type == "reg")
            return Data_Load("select org, line, sum(case when reg_cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') then amount else 0 end) as cnt from reg group by org, line", "reg");
        else
            return Data_Load("select org, area, count(1) as cnt from bis where reg_cat_id = 'LineUp' and status = 'In The Shop' group by org, area order by org, area", "reg");
    }

    public DataTable ReportByOrgArea(string type, string org)
    {
        if (type == "In and Started")
            return Data_Load(String.Format("select area, count(1) as cnt from bis where reg_cat_id = 'In and Started' and org = '{0}' and status = 'In and Started' group by area order by area", org), "reg");
        else if (type == "Comp Resign")
            return Data_Load(String.Format("select area, count(1) as cnt from bis where reg_cat_id = 'Comp Resign' and org = '{0}' and status = 'Comp Resign' group by area order by area", org), "reg");
        else if (type == "First Service")
            return Data_Load(String.Format("select area, count(1) as cnt from bis where reg_cat_id = 'First Service' and org = '{0}' and status = 'First Service' group by area order by area", org), "reg");
        else if (type == "Purif Start")
            return Data_Load(String.Format("select line, count(1) as cnt from bis where reg_cat_id = 'Purif' and org = '{0}' and status = 'Purif Start' group by line order by line", org), "reg");
        else if (type == "Paid Start")
            return Data_Load(String.Format("select line, count(1) as cnt from bis where reg_cat_id = 'Paid Start' and org = '{0}' and status = 'Started' group by line order by line", org), "reg");
        else if (type == "reg")
            return Data_Load(String.Format("select line, sum(case when reg_cat_id = 'LineUp' and org = '{0}' and status in ('GI Invoiced', 'GI Confirmed') then amount else 0 end) as cnt from reg group by line", org), "reg");
        else
            return Data_Load(String.Format("select area, count(1) as cnt from bis where reg_cat_id = 'LineUp' and org = '{0}' and status = 'In The Shop' group by area order by area", org), "reg");
    }

    public DataTable ReportBISAreaFallOff()
    {
        return Data_Load("select org, area, count(1) as cnt from bis where reg_cat_id = 'LineUp' and status = 'Named' group by org, area order by org, area", "reg");
    }

    public DataTable Calendar(string head, string org)
    {
        return Data_Load(String.Format("SELECT appt, status, name, service, reg, amount, line, rank, scheduled_type, notes from reg where reg_cat_id in('Future') and org = '{1}' order by appt ASC", head, org), "reg");
    }

    public DataTable CurrentWeek(string org)
    {
        return Data_Load(String.Format("SELECT appt, status, name, service, reg, amount, line, rank, scheduled_type, reg_cat_id, notes from reg where status in('Open Cycle', 'GI Confirmed', 'GI Invoiced') and org = '{0}' order by rank, appt ASC", org), "reg");
    }

    public DataTable Archive(string org)
    {
        return Data_Load(String.Format("SELECT appt, status, name, service, reg, amount, line, rank, scheduled_type, notes from reg where reg_cat_id in('Archive') and org = '{0}' order by appt DESC", org), "reg");
    }

    public DataTable Inv_Table()
    {

        CultureInfo currentUI = Thread.CurrentThread.CurrentUICulture;

        if (currentUI.ToString() == "de")
        {
            return Data_Load("select item.desc1 + ' ' + item.desc2 as item, inv.amt_paid, fsm_name, inv_date, addo.home_phone as phone, addo.first_name + ' ' "
                + "+ addo.last_name as name, addo.addo_id from dbo.pers_inv_detail as inv INNER JOIN pers_invoice as i on i.pers_inv_id = inv.pers_inv_id "
                + "INNER JOIN pers_addo as addo ON inv.addo_id = addo.addo_id "
                + "INNER JOIN item_ref as item on inv.master_item_id = item.item_id "
                + "WHERE i.inv_type in ('GUTSCHRIFT', 'CASH', 'VORAUSZAHL.') and addo.last_name > '' and addo.first_name > '' and inv.amt_paid > 0 order by inv_date desc", "cf");
        }
        else
        {
            return Data_Load("select item.desc1 + ' ' + item.desc2 as item, inv.amt_paid, fsm_name, inv_date, addo.home_phone as phone, addo.first_name + ' ' "
                + "+ addo.last_name as name, addo.addo_id from dbo.pers_inv_detail as inv INNER JOIN pers_invoice as i on i.pers_inv_id = inv.pers_inv_id "
                + "INNER JOIN pers_addo as addo ON inv.addo_id = addo.addo_id "
                + "INNER JOIN item_ref as item on inv.master_item_id = item.item_id "
                + "WHERE i.inv_type in ('DONATION', 'CASH', 'CREDIT') and addo.last_name > '' and addo.first_name > '' and inv.amt_paid > 0 order by inv_date desc", "cf");
        };

    }

    public DataTable FPPP(string org)
    {

        if (org == "Day")
            return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
        else if (org == "Fdn")
            return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
        else
            return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                + "INNER JOIN item_ref as item on fppp.item_id = item.item_id order by create_time desc", org), "cf");
    }

    public DataTable FilterFPPP(string org, string type)
    {

        if (type == "Purif")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_id in (-54722, -9352, -7439, -14009) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_id in (-54722, -9352, -7439, -14009) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item.item_id in (-54722, -9352, -7439, -14009) "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "SRD")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_id in (-54677) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_id in (-54677) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item.item_id in (-54677) "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "HGC")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type = 1 AND item.item_id NOT in (-54722, -9352,  -7439, -14009, -54677) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type = 1 AND item.item_id NOT in (-54722, -9352,  -7439, -14009, -54677) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item_type = 1 AND item.item_id NOT in (-54722, -9352,  -7439, -14009, -54677) "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "ACADEMY")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_type = 2 "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_type = 2 "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item.item_type = 2 "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "Div 6 Training")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type = 6 "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type = 6 "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item_type = 6 "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "Div 6 Processing")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type = 7 "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type = 7 "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item_type = 7 "
                    + "order by create_time desc", org), "cf");
        }
        else
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "order by create_time desc", org), "cf");
        }
    }

    public DataTable FilterFP(string org, string type)
    {

        if (type == "Purif")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_id in (-54722, -9352, -7439, -14009) and fppp.fp = '1' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_id in (-54722, -9352, -7439, -14009) and fppp.fp = '1'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item.item_id in (-54722, -9352, -7439, -14009) and fppp.fp = '1'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "SRD")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_id in (-54677) and fppp.fp = '1'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_id in (-54677) and fppp.fp = '1'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item.item_id in (-54677) and fppp.fp = '1'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "HGC")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type = 1 AND item.item_id NOT in (-54722, -9352,  -7439, -14009, -54677) and fppp.fp = '1'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type = 1 AND item.item_id NOT in (-54722, -9352,  -7439, -14009, -54677) and fppp.fp = '1'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item_type = 1 AND item.item_id NOT in (-54722, -9352,  -7439, -14009, -54677) and fppp.fp = '1' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "ACAD")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_type = 2  and fppp.fp = '1' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_type = 2  and fppp.fp = '1' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item.item_type = 2  and fppp.fp = '1' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "DIV6")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type in (6, 7)  and fppp.fp = '1' "
                    + "AND item.ITEM_ID > -45632 "
                    + "and desc1 not like '%EXTENSION%' AND DESC2 NOT LIKE '%EXTENSION%' "
                    + "and desc1 not like '%BOOK%COURSE%' AND DESC2 NOT LIKE '%BOOK%COURSE%' "
                    + "and desc1 not like '%LECTURE%COURSE%' AND DESC2 NOT LIKE '%LECTURE%COURSE%' "
                    + "and desc1 not like '%PRO%COURSE%' AND DESC2 NOT LIKE '%PRO%COURSE%' "
                    + "and desc1 not like '%ACC%' AND DESC2 NOT LIKE '%ACC%' "
                    + "AND ITEM.ITEM_ID NOT IN (-31189, -33328) "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type in (6, 7)  and fppp.fp = '1' "
                    + "AND item.ITEM_ID > -45632 "
                    + "and desc1 not like '%EXTENSION%' AND DESC2 NOT LIKE '%EXTENSION%' "
                    + "and desc1 not like '%BOOK%COURSE%' AND DESC2 NOT LIKE '%BOOK%COURSE%' "
                    + "and desc1 not like '%LECTURE%COURSE%' AND DESC2 NOT LIKE '%LECTURE%COURSE%' "
                    + "and desc1 not like '%PRO%COURSE%' AND DESC2 NOT LIKE '%PRO%COURSE%' "
                    + "and desc1 not like '%ACC%' AND DESC2 NOT LIKE '%ACC%' "
                    + "AND ITEM.ITEM_ID NOT IN (-31189, -33328) "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item_type in (6, 7)  and fppp.fp = '1' "
                    + "AND item.ITEM_ID > -45632 "
                    + "and desc1 not like '%EXTENSION%' AND DESC2 NOT LIKE '%EXTENSION%' "
                    + "and desc1 not like '%BOOK%COURSE%' AND DESC2 NOT LIKE '%BOOK%COURSE%' "
                    + "and desc1 not like '%LECTURE%COURSE%' AND DESC2 NOT LIKE '%LECTURE%COURSE%' "
                    + "and desc1 not like '%PRO%COURSE%' AND DESC2 NOT LIKE '%PRO%COURSE%' "
                    + "and desc1 not like '%ACC%' AND DESC2 NOT LIKE '%ACC%' "
                    + "AND ITEM.ITEM_ID NOT IN (-31189, -33328) "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "GAK")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id  "
                    + "WHERE item_type in (6, 7) AND (item.ITEM_ID <= -45632  "
                    + " or desc1 like '%EXTENSION%' OR DESC2 LIKE '%EXTENSION%' "
                    + " OR desc1 like '%BOOK%COURSE%' OR DESC2 LIKE '%BOOK%COURSE%' "
                    + " OR desc1 like '%LECTURE%COURSE%' OR DESC2 LIKE '%LECTURE%COURSE%' "
                    + " OR desc1 like '%PRO%COURSE%' OR DESC2 LIKE '%PRO%COURSE%' "
                    + " OR desc1 like '%ACC%' OR DESC2 LIKE '%ACC%' "
                    + " OR ITEM.ITEM_ID IN (-31189, -33328) "
                    + ") AND ITEM.ITEM_ID NOT IN (-54694, -54696) and fppp.fp = '1' fppp.org_id = {0} "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item_type in (6, 7) AND (item.ITEM_ID <= -45632  "
                    + " or desc1 like '%EXTENSION%' OR DESC2 LIKE '%EXTENSION%' "
                    + " OR desc1 like '%BOOK%COURSE%' OR DESC2 LIKE '%BOOK%COURSE%' "
                    + " OR desc1 like '%LECTURE%COURSE%' OR DESC2 LIKE '%LECTURE%COURSE%' "
                    + " OR desc1 like '%PRO%COURSE%' OR DESC2 LIKE '%PRO%COURSE%' "
                    + " OR desc1 like '%ACC%' OR DESC2 LIKE '%ACC%' "
                    + " OR ITEM.ITEM_ID IN (-31189, -33328) "
                    + ") AND ITEM.ITEM_ID NOT IN (-54694, -54696) and fppp.fp = '1' fppp.org_id = {0} "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item_type in (6, 7) AND (item.ITEM_ID <= -45632  "
                    + " or desc1 like '%EXTENSION%' OR DESC2 LIKE '%EXTENSION%' "
                    + " OR desc1 like '%BOOK%COURSE%' OR DESC2 LIKE '%BOOK%COURSE%' "
                    + " OR desc1 like '%LECTURE%COURSE%' OR DESC2 LIKE '%LECTURE%COURSE%' "
                    + " OR desc1 like '%PRO%COURSE%' OR DESC2 LIKE '%PRO%COURSE%' "
                    + " OR desc1 like '%ACC%' OR DESC2 LIKE '%ACC%' "
                    + " OR ITEM.ITEM_ID IN (-31189, -33328) "
                    + ") AND ITEM.ITEM_ID NOT IN (-54694, -54696) and fppp.fp = '1' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
        else
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0}  and fppp.fp = '1' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0}  and fppp.fp = '1' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.fp = '1' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
    }

    public DataTable FilterPP(string org, string type)
    {

        if (type == "Purif")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_id in (-54722, -9352, -7439, -14009) and fppp.fp = '0' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_id in (-54722, -9352, -7439, -14009) and fppp.fp = '0'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item.item_id in (-54722, -9352, -7439, -14009) and fppp.fp = '0'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "SRD")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_id in (-54677) and fppp.fp = '0'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_id in (-54677) and fppp.fp = '0'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item.item_id in (-54677) and fppp.fp = '0'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "HGC")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type = 1 AND item.item_id NOT in (-54722, -9352,  -7439, -14009, -54677) and fppp.fp = '0'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type = 1 AND item.item_id NOT in (-54722, -9352,  -7439, -14009, -54677) and fppp.fp = '0'  "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item_type = 1 AND item.item_id NOT in (-54722, -9352,  -7439, -14009, -54677) and fppp.fp = '0' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "ACAD")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_type = 2  and fppp.fp = '0' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item.item_type = 2  and fppp.fp = '0' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item.item_type = 2  and fppp.fp = '0' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "DIV6")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type in (6, 7)  and fppp.fp = '0' "
                    + "AND item.ITEM_ID > -45632 "
                    + "and desc1 not like '%EXTENSION%' AND DESC2 NOT LIKE '%EXTENSION%' "
                    + "and desc1 not like '%BOOK%COURSE%' AND DESC2 NOT LIKE '%BOOK%COURSE%' "
                    + "and desc1 not like '%LECTURE%COURSE%' AND DESC2 NOT LIKE '%LECTURE%COURSE%' "
                    + "and desc1 not like '%PRO%COURSE%' AND DESC2 NOT LIKE '%PRO%COURSE%' "
                    + "and desc1 not like '%ACC%' AND DESC2 NOT LIKE '%ACC%' "
                    + "AND ITEM.ITEM_ID NOT IN (-31189, -33328) "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0} "
                    + "and item_type in (6, 7)  and fppp.fp = '0' "
                    + "AND item.ITEM_ID > -45632 "
                    + "and desc1 not like '%EXTENSION%' AND DESC2 NOT LIKE '%EXTENSION%' "
                    + "and desc1 not like '%BOOK%COURSE%' AND DESC2 NOT LIKE '%BOOK%COURSE%' "
                    + "and desc1 not like '%LECTURE%COURSE%' AND DESC2 NOT LIKE '%LECTURE%COURSE%' "
                    + "and desc1 not like '%PRO%COURSE%' AND DESC2 NOT LIKE '%PRO%COURSE%' "
                    + "and desc1 not like '%ACC%' AND DESC2 NOT LIKE '%ACC%' "
                    + "AND ITEM.ITEM_ID NOT IN (-31189, -33328) "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item_type in (6, 7)  and fppp.fp = '0' "
                    + "AND item.ITEM_ID > -45632 "
                    + "and desc1 not like '%EXTENSION%' AND DESC2 NOT LIKE '%EXTENSION%' "
                    + "and desc1 not like '%BOOK%COURSE%' AND DESC2 NOT LIKE '%BOOK%COURSE%' "
                    + "and desc1 not like '%LECTURE%COURSE%' AND DESC2 NOT LIKE '%LECTURE%COURSE%' "
                    + "and desc1 not like '%PRO%COURSE%' AND DESC2 NOT LIKE '%PRO%COURSE%' "
                    + "and desc1 not like '%ACC%' AND DESC2 NOT LIKE '%ACC%' "
                    + "AND ITEM.ITEM_ID NOT IN (-31189, -33328) "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
        else if (type == "GAK")
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id  "
                    + "WHERE item_type in (6, 7) AND (item.ITEM_ID <= -45632  "
                    + " or desc1 like '%EXTENSION%' OR DESC2 LIKE '%EXTENSION%' "
                    + " OR desc1 like '%BOOK%COURSE%' OR DESC2 LIKE '%BOOK%COURSE%' "
                    + " OR desc1 like '%LECTURE%COURSE%' OR DESC2 LIKE '%LECTURE%COURSE%' "
                    + " OR desc1 like '%PRO%COURSE%' OR DESC2 LIKE '%PRO%COURSE%' "
                    + " OR desc1 like '%ACC%' OR DESC2 LIKE '%ACC%' "
                    + " OR ITEM.ITEM_ID IN (-31189, -33328) "
                    + ") AND ITEM.ITEM_ID NOT IN (-54694, -54696) and fppp.fp = '0' fppp.org_id = {0} "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item_type in (6, 7) AND (item.ITEM_ID <= -45632  "
                    + " or desc1 like '%EXTENSION%' OR DESC2 LIKE '%EXTENSION%' "
                    + " OR desc1 like '%BOOK%COURSE%' OR DESC2 LIKE '%BOOK%COURSE%' "
                    + " OR desc1 like '%LECTURE%COURSE%' OR DESC2 LIKE '%LECTURE%COURSE%' "
                    + " OR desc1 like '%PRO%COURSE%' OR DESC2 LIKE '%PRO%COURSE%' "
                    + " OR desc1 like '%ACC%' OR DESC2 LIKE '%ACC%' "
                    + " OR ITEM.ITEM_ID IN (-31189, -33328) "
                    + ") AND ITEM.ITEM_ID NOT IN (-54694, -54696) and fppp.fp = '0' fppp.org_id = {0} "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
                    + "WHERE item_type in (6, 7) AND (item.ITEM_ID <= -45632  "
                    + " or desc1 like '%EXTENSION%' OR DESC2 LIKE '%EXTENSION%' "
                    + " OR desc1 like '%BOOK%COURSE%' OR DESC2 LIKE '%BOOK%COURSE%' "
                    + " OR desc1 like '%LECTURE%COURSE%' OR DESC2 LIKE '%LECTURE%COURSE%' "
                    + " OR desc1 like '%PRO%COURSE%' OR DESC2 LIKE '%PRO%COURSE%' "
                    + " OR desc1 like '%ACC%' OR DESC2 LIKE '%ACC%' "
                    + " OR ITEM.ITEM_ID IN (-31189, -33328) "
                    + ") AND ITEM.ITEM_ID NOT IN (-54694, -54696) and fppp.fp = '0' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
        else
        {
            if (org == "Day")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0}  and fppp.fp = '0' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
            else if (org == "Fdn")
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = {0}  and fppp.fp = '0' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
            else
                return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                    + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.fp = '0' "
                    + " and mail_stat not in (4,6) "
                    + "order by create_time desc", org), "cf");
        }
    }

    public DataTable Account(string org)
    {
        if (org == "Day")
            return Data_Load(String.Format("select sum(fppp.amt_paid) as on_account,  addo.first_name + ' ' + addo.last_name + ' ' + corp_name as name, addo.addo_id, fppp.org_id "
                + "from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = '{0}' GROUP BY addo.first_name + ' ' + addo.last_name + ' ' + corp_name, addo.addo_id, fppp.org_id ORDER BY on_account desc", System.Configuration.ConfigurationManager.AppSettings["orgid_day"]), "cf");
        else if (org == "Fdn")
            return Data_Load(String.Format("select sum(fppp.amt_paid) as on_account,  addo.first_name + ' ' + addo.last_name + ' ' + corp_name as name, addo.addo_id, fppp.org_id "
                + "from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where fppp.org_id = '{0}' GROUP BY addo.first_name + ' ' + addo.last_name + ' ' + corp_name, addo.addo_id, fppp.org_id ORDER BY on_account desc", System.Configuration.ConfigurationManager.AppSettings["orgid_fdn"]), "cf");
        else
            return Data_Load(String.Format("select sum(fppp.amt_paid) as on_account,  addo.first_name + ' ' + addo.last_name + ' ' + corp_name as name, addo.addo_id, fppp.org_id "
                + "from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                + "INNER JOIN item_ref as item on fppp.item_id = item.item_id GROUP BY addo.first_name + ' ' + addo.last_name + ' ' + corp_name, addo.addo_id, fppp.org_id ORDER BY on_account desc", org), "cf");
    }

    public DataTable Account_Search(string Search, string Text)
    {
        return Data_Load(String.Format("select sum(fppp.amt_paid) as on_account,  addo.first_name + ' ' + addo.last_name + ' ' + corp_name as name, addo.addo_id, fppp.org_id "
            + "from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
            + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where addo.first_name + ' ' + addo.last_name + ' ' + addo.corp_name like '%{0}%' "
            + "GROUP BY addo.first_name + ' ' + addo.last_name + ' ' + corp_name, addo.addo_id, fppp.org_id ORDER BY name ", Text), "cf");
    }

    public DataTable TTL()
    {
        return Data_Load("select name, addo_id, sum(inv_total) as tot_paid from dbo.pers_invoice where inv_type in ('GUTSCHRIFT', 'CASH', 'VORAUSZAHL.') group by name, addo_id having sum(inv_total) > 50 order by tot_paid desc", "cf");
    }

    public DataTable Appt(string day)
    {
        if (day == "")
        {
            return Data_Load("select * from regAppt where day = format(getdate(), 'd-MMM-yyyy')", "reg");
        }
        else
        {
            return Data_Load(String.Format("select * from regAppt where day = '{0}' order by hour_txt", day), "reg");
        }
    }

    public DataTable addo()
    {
        return Data_Load("select addo_id, first_name + ' ' + last_name as name, city, " +
            "CASE WHEN hphone_status = 1 and len(home_phone) > 7 THEN home_phone WHEN aphone_status = 1 and len(alt_phone) > 7 THEN alt_phone WHEN wphone_status = 1 and len(work_phone) > 7 THEN work_phone  " +
            "	ELSE NULL    " +
            "END as phone  " +
        "from dbo.pers_addo where mail_stat in (1,2,5,7,8) and len(first_name) > 0 and len(last_name) > 0 and addo_id > 0  ", "cf");
    }

    public DataTable Search(string status, string Org, string Search, string Text)
    {

        if (Org == "Combined")
            return Data_Load(String.Format("SELECT * from reg WHERE status = '{0}' and {1} like '%{2}%' and reg_cat_id <> 'Archive'", status, Search, Text), "reg");
        return Data_Load(String.Format("SELECT * from reg WHERE status = '{0}' and org = '{1}' and {2} like '%{3}%' and reg_cat_id <> 'Archive'", status, Org, Search, Text), "reg");

    }

    public DataTable Search_BIS(string status, string Org, string Search, string Text)
    {
        if (Org == "Combined")
            return Data_Load(String.Format("SELECT * from bis WHERE status = '{0}' and {1} like '%{2}%' and reg_cat_id <> 'Archive'", status, Search, Text), "reg");
        return Data_Load(String.Format("SELECT * from bis WHERE status = '{0}' and org = '{1}' and {2} like '%{3}%' and reg_cat_id <> 'Archive'", status, Org, Search, Text), "reg");
    }

    public DataTable Search_BIS_log(string Org, string Search, string Text)
    {
        if (Org == "Combined")
            return Data_Load(String.Format("SELECT * from bis WHERE {0} like '%{1}%' and reg_cat_id <> 'Archive'", Search, Text), "reg");
        return Data_Load(String.Format("SELECT * from bis WHERE org = '{0}' and {1} like '%{2}%' and reg_cat_id <> 'Archive'", Org, Search, Text), "reg");
    }

    public DataTable FilterBIS(string org, string area, string regcat)
    {
        if (org == "Combined")
        {
            if (area == "GAK")
            {
                return Data_Load(String.Format("SELECT * from bis WHERE area in ('KNOW', 'GAK') and reg_cat_id = '{2}'", org, area, regcat), "reg");
            }
            else if (area == "ACAD")
            {
                return Data_Load(String.Format("SELECT * from bis WHERE area in ('ACAD', 'INTERN') and reg_cat_id = '{2}'", org, area, regcat), "reg");
            }
            else if (area == "DIV6")
            {
                return Data_Load(String.Format("SELECT * from bis WHERE area in ('PE', 'DIV6', 'LI', 'STCC', 'DN', 'HQS') and reg_cat_id = '{2}'", org, area, regcat), "reg");
            }
            else
            {
                return Data_Load(String.Format("SELECT * from bis WHERE area like '%{1}%' and reg_cat_id = '{2}'", org, area, regcat), "reg");
            }

        }
        else
        {

            if (area == "GAK")
            {
                return Data_Load(String.Format("SELECT * from bis WHERE area in ('KNOW', 'GAK') and reg_cat_id = '{2}' and org = '{0}' ", org, area, regcat), "reg");
            }
            else if (area == "ACAD")
            {
                return Data_Load(String.Format("SELECT * from bis WHERE area in ('ACAD', 'INTERN') and reg_cat_id = '{2}' and org = '{0}' ", org, area, regcat), "reg");
            }
            else if (area == "DIV6")
            {
                return Data_Load(String.Format("SELECT * from bis WHERE area in ('PE', 'DIV6', 'LI', 'STCC', 'DN', 'HQS') and reg_cat_id = '{2}' and org = '{0}' ", org, area, regcat), "reg");
            }
            else
            {
                return Data_Load(String.Format("SELECT * from bis WHERE area like '%{1}%' and reg_cat_id = '{2}' and org = '{0}' ", org, area, regcat), "reg");
            }

        }
    }

    public DataTable Search_reg_log(string Org, string Search, string Text)
    {
        if (Org == "Combined")
            return Data_Load(String.Format("SELECT * from reg WHERE {0} like '%{1}%' and reg_cat_id <> 'Archive'", Search, Text), "reg");
        return Data_Load(String.Format("SELECT * from reg WHERE org = '{0}' and {1} like '%{2}%' and reg_cat_id <> 'Archive'", Org, Search, Text), "reg");
    }

    public DataTable Search_Combo_BIS(string Search, string Text)
    {
        return Data_Load(String.Format("SELECT * from bis WHERE reg_cat_id <> 'Archive' and {0} like '%{1}%'", Search, Text), "reg");
    }

    public DataTable ArchiveSearch(string Search, string Text, string Org)
    {
        if (Org == "Combined")
            return Data_Load(String.Format("SELECT * from reg WHERE reg_cat_id = 'Archive' and {0} like '%{1}%'", Search, Text), "reg");
        return Data_Load(String.Format("SELECT * from reg WHERE reg_cat_id = 'Archive' and {0} like '%{1}%' and org = '{2}' ", Search, Text, Org), "reg");
    }

    public DataTable ArchiveBISSearch(string Search, string Text, string Org, string Status)
    {
        if (Org == "Combined")
            return Data_Load(String.Format("SELECT * from bis WHERE reg_cat_id = 'Archive' and {0} like '%{1}%' and Status = '{2}'", Search, Text, Status), "reg");
        return Data_Load(String.Format("SELECT * from bis WHERE reg_cat_id = 'Archive' and {0} like '%{1}%' and Status = '{2}' and org = '{3}' ", Search, Text, Status, Org), "reg");

    }

    public DataTable ArchiveWESearch(string Search, string Text, string Org)
    {
        if (Org == "Combined")
            return Data_Load(String.Format("SELECT * from reg WHERE reg_cat_id = 'Archive' and {0} = '{1}'", Search, Text), "reg");
        return Data_Load(String.Format("SELECT * from reg WHERE reg_cat_id = 'Archive' and {0} = '{1}' and org = '{2}'", Search, Text, Org), "reg");
    }

    public DataTable ArchiveBISWESearch(string Search, string Text, string Org, string Status)
    {
        if (Org == "Combined")
            return Data_Load(String.Format("SELECT * from bis WHERE reg_cat_id = 'Archive' and {0} = '{1}' and Status = '{2}' ", Search, Text, Status), "reg");
        return Data_Load(String.Format("SELECT * from bis WHERE reg_cat_id = 'Archive' and {0} = '{1}' and org = '{2}' and Status = '{3}'", Search, Text, Org, Status), "reg");
    }

    public DataTable ArchiveWE_FilterSearch(string Search, string Text, string Search2, string Text2, string Org)
    {
        if (Org == "Combined")
            return Data_Load(String.Format("SELECT * from reg WHERE reg_cat_id = 'Archive' and {0} = '{1}' and {2} like '%{3}%'", Search, Text, Search2, Text2), "reg");
        return Data_Load(String.Format("SELECT * from reg WHERE reg_cat_id = 'Archive' and {0} = '{1}' and {2} like '%{3}%' and org = '{4}'", Search, Text, Search2, Text2, Org), "reg");
    }

    public DataTable ArchiveBISWE_FilterSearch(string Search, string Text, string Search2, string Text2, string Org, string Status)
    {
        if (Org == "Combined")
            return Data_Load(String.Format("SELECT * from bis WHERE reg_cat_id = 'Archive' and {0} = '{1}' and {2} like '%{3}%' and Status = '{4}'", Search, Text, Search2, Text2, Status), "reg");
        return Data_Load(String.Format("SELECT * from bis WHERE reg_cat_id = 'Archive' and {0} = '{1}' and {2} like '%{3}%' and org = '{4}' and Status = '{5}'", Search, Text, Search2, Text2, Org, Status), "reg");
    }

    public DataTable Search_Combo(string Search, string Text)
    {
        return Data_Load(String.Format("SELECT * from reg WHERE reg_cat_id <> 'Archive' and {0} like '%{1}%'", Search, Text), "reg");
    }

    public DataTable Search_FPPP(string Search, String Text)
    {
        if (Search == "Name")
        {
            return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
                + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
                + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where addo.first_name + ' ' + addo.last_name like '%{0}%'", Text), "cf");
        }
        else if (Search == "Service")
        {
            return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.home_phone as phone, addo.first_name + ' ' "
            + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id, fppp.org_id from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
            + "INNER JOIN item_ref as item on fppp.item_id = item.item_id where item.desc1 + ' ' + item.desc2 like '%{0}%'", Text), "cf");
        }
        else
            return null;
    }

    public DataTable Search_Inv(string Search, String Text)
    {



        if (Search == "Name")
        {
            return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, inv.amt_paid, fsm_name, inv_date, addo.home_phone as phone, addo.first_name + ' ' "
             + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id from dbo.pers_inv_detail as inv INNER JOIN pers_invoice as i on i.pers_inv_id = inv.pers_inv_id "
             + "INNER JOIN pers_addo as addo ON inv.addo_id = addo.addo_id "
             + "INNER JOIN item_ref as item on inv.master_item_id = item.item_id "
             + "WHERE i.inv_type in ('DONATION', 'CASH', 'CREDIT') and addo.last_name > '' and addo.first_name > '' and inv.amt_paid > 0 "
             + "and addo.first_name + ' ' + addo.last_name like '%{0}%'", Text), "cf");
        }
        else if (Search == "Service")
        {
            return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, inv.amt_paid, fsm_name, inv_date, addo.home_phone as phone, addo.first_name + ' ' "
             + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id from dbo.pers_inv_detail as inv INNER JOIN pers_invoice as i on i.pers_inv_id = inv.pers_inv_id "
             + "INNER JOIN pers_addo as addo ON inv.addo_id = addo.addo_id "
             + "INNER JOIN item_ref as item on inv.master_item_id = item.item_id "
             + "WHERE i.inv_type in ('DONATION', 'CASH', 'CREDIT') and addo.last_name > '' and addo.first_name > '' and inv.amt_paid > 0 "
             + "and item.desc1 + ' ' + item.desc2 like '%{0}%'", Text), "cf");
        }
        else if (Search == "FSM")
        {
            return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, inv.amt_paid, fsm_name, inv_date, addo.home_phone as phone, addo.first_name + ' ' "
             + "+ addo.last_name + ' ' + addo.corp_name as name, addo.addo_id from dbo.pers_inv_detail as inv INNER JOIN pers_invoice as i on i.pers_inv_id = inv.pers_inv_id "
             + "INNER JOIN pers_addo as addo ON inv.addo_id = addo.addo_id "
             + "INNER JOIN item_ref as item on inv.master_item_id = item.item_id "
             + "WHERE i.inv_type in ('DONATION', 'CASH', 'CREDIT') and addo.last_name > '' and addo.first_name > '' and inv.amt_paid > 0 "
             + "and fsm_name like '%{0}%'", Text), "cf");
        }
        else
        {
            return null;
        }


    }

    public DataTable Search_TTL(string Search, String Text)
    {
        return Data_Load(String.Format("select name, addo_id, sum(inv_total) as tot_paid from dbo.pers_invoice where inv_type in ('GUTSCHRIFT', 'CASH', 'VORAUSZAHL.') "
            + " and {0} like '%{1}%' group by name, addo_id having sum(inv_total) > 50 order by tot_paid desc", Search, Text), "cf");
    }

    public DataTable Search_Addo(string Search, String Text)
    {
        return Data_Load(String.Format("select addo_id, first_name + ' ' + last_name as name, city from dbo.pers_addo where mail_stat in (1,2,5,7,8) and len(first_name) > 0"
            + " and (first_name + ' ' + last_name like '%{1}%' or city like '%{1}%')", Search, Text), "cf");
    }

    public DataTable Report_By_Lines(string Name)
    {
        if (Name == "")
            return Data_Load(String.Format("select line, count(1) as num from reg where reg_cat_id = 'archive' group by line ", Name), "reg");
        else
            return Data_Load(String.Format("select line, count(1) as num from reg where reg like '%{0}%' and reg_cat_id = 'archive' group by line ", Name), "reg");
    }

    public DataTable GetDailyRegGI(string type, string reg)
    {

        if (type == "thisweek")
            return Data_Load(String.Format("SELECT desc2 as week_day, h.desc1 as day_desc, COALESCE(d.tot,0) as tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                    "SELECT datename(dw, tm) AS week_day, sum(amount) AS tot FROM reg p " +
                    "WHERE reg_Cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') and reg like '%{0}%' and tm > getdate() - 8  " +
                    "GROUP BY datename(dw, tm) " +
                    "UNION " +
                    "SELECT 'ThursdayAfter2' as week_day, sum(amount)  AS tot FROM reg WHERE reg_Cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed')  and reg like '%{0}%' and tm > getdate() - 8 " +
                    "and datename(dw,tm) = 'Thursday' and tm < getdate()  " +
                    "GROUP BY datename(dw, tm), day(tm) " +
                    "UNION " +
                    "SELECT 'ThursdayBefore2' AS week_day, sum(amount) AS tot FROM reg WHERE reg_Cat_id = 'LineUp' and status  in ('GI Invoiced', 'GI Confirmed') and reg like '%{0}%'  and tm > getdate() - 8 " +
                    "and datename(dw,tm) = 'Thursday' and tm >= getdate()   " +
                    "GROUP BY datename(dw, tm), day(tm) ) d " +
                    "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3 ", reg), "reg");
        else if (type == "thisweekinv")
            return Data_Load(String.Format("SELECT desc2 as week_day, h.desc1 as day_desc, COALESCE(d.tot,0) as tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                    "SELECT datename(dw, tm) AS week_day, sum(amount) AS tot FROM reg p " +
                    "WHERE reg_Cat_id = 'LineUp' and status in ('GI Invoiced')  and reg like '%{0}%' and tm > getdate() - 8  " +
                    "GROUP BY datename(dw, tm) " +
                    "UNION " +
                    "SELECT 'ThursdayAfter2' as week_day, sum(amount)  AS tot FROM reg WHERE reg_Cat_id = 'LineUp' and status in ('GI Invoiced')  and reg like '%{0}%' and tm > getdate() - 8 " +
                    "and datename(dw,tm) = 'Thursday' and tm < getdate()  " +
                    "GROUP BY datename(dw, tm), day(tm) " +
                    "UNION " +
                    "SELECT 'ThursdayBefore2' AS week_day, sum(amount) AS tot FROM reg WHERE reg_Cat_id = 'LineUp' and status  in ('GI Invoiced')  and reg like '%{0}%' and tm > getdate() - 8 " +
                    "and datename(dw,tm) = 'Thursday' and tm >= getdate()   " +
                    "GROUP BY datename(dw, tm), day(tm) ) d " +
                    "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3 ", reg), "reg");
        else
            return Data_Load(String.Format(

                    "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                    "SELECT datename(dw, appt) AS week_day, sum(amount) AS tot FROM reg p WHERE reg_Cat_id = 'Archive' and reg like '%{0}%'  and tm > getdate() - 8  " +
                    "and datename(dw,appt) <> 'Thursday' GROUP BY datename(dw, appt) " +
                    "UNION " +
                    "SELECT 'ThursdayAfter2' as week_day, sum(amount) AS tot FROM reg WHERE reg_Cat_id = 'Archive' and reg like '%{0}%'  and tm > getdate() - 8 " +
                    "and datename(dw,appt) = 'Thursday' and day(appt) < day(tm) GROUP BY datename(dw, appt), day(appt)" +
                    "UNION " +
                    "SELECT 'ThursdayBefore2' AS week_day, sum(amount) AS tot FROM reg WHERE reg_Cat_id = 'Archive' and reg like '%{0}%'  and tm > getdate() - 8  " +
                    "and datename(dw,appt) = 'Thursday' and day(appt) = day(tm) GROUP BY datename(dw, appt), day(appt)) d " +
                    "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", reg), "reg");

    }

    public DataTable GetDailyInStarted(string type)
    {
        if (type == "thisweek")
            return Data_Load(
                     "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                     "SELECT datename(dw, weekend) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'In and Started' " +
                    " and status = 'In and Started' and weekend > getdate() - 8  " +
                     "and datename(dw,weekend) <> 'Thursday' GROUP BY datename(dw, weekend) " +
                     "UNION " +
                     "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'In and Started' and status = 'In and Started' and weekend > getdate() - 8 " +
                     "and datename(dw,weekend) = 'Thursday' and day(weekend) < day(getdate())  GROUP BY datename(dw, weekend), day(weekend) " +
                     "UNION " +
                     "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'In and Started' and status = 'In and Started' and weekend > getdate() - 8  " +
                     "and datename(dw,weekend) = 'Thursday' and day(weekend) >= day(getdate())  GROUP BY datename(dw, weekend), day(weekend) ) d " +
                     "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", "reg");
        else if (type == "thisweeksched")
            return Data_Load(
                     "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                     "SELECT datename(dw, weekend) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'In and Started' " +
                    " and status = 'Scheduled' and weekend > getdate() - 8  " +
                     "and datename(dw,weekend) <> 'Thursday' GROUP BY datename(dw, weekend) " +
                     "UNION " +
                     "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'In and Started' " +
                    "and status = 'Scheduled' and weekend > getdate() - 8 " +
                     "and datename(dw,weekend) = 'Thursday' and day(weekend) < day(getdate())  GROUP BY datename(dw, weekend), day(weekend) " +
                     "UNION " +
                     "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'In and Started' " +
                    "and status = 'Scheduled' and weekend > getdate() - 8  " +
                     "and datename(dw,weekend) = 'Thursday' and day(weekend) >= day(getdate())  GROUP BY datename(dw, weekend), day(weekend) ) d " +
                     "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", "reg");
        return Data_Load(

                 "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                 "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'Archive' and status = 'In and Started' and weekend > getdate() - 8  " +
                 "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                 "UNION " +
                 "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and status = 'In and Started' and weekend > getdate() - 8 " +
                 "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(weekend) GROUP BY datename(dw, scheduled), day(scheduled)" +
                 "UNION " +
                 "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and status = 'In and Started' and weekend > getdate() - 8  " +
                 "and datename(dw,scheduled) = 'Thursday' and day(scheduled) = day(weekend) GROUP BY datename(dw, scheduled), day(scheduled)) d " +
                 "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", "reg");

    }

    public DataTable GetDailyCategory(string type, string category, string status, string we, string org)
    {

        if (org == "Combined")
        {

            if (type == "thisweek")
                return Data_Load(String.Format(
                    " select datename(dw, first_date) as weekDay, day(first_date) as dayNbr, convert(varchar, first_date, 23) as invDate, count(distinct org + name + area + service) as tot " +
                    " from (select org, name, area, service, min(scheduled) as first_date from bis where reg_cat_id = '{0}' and status = '{1}' group by org, name, area, service) as b " +
                    " group by datename(dw, first_date), day(first_date), convert(varchar, first_date, 23) " +
                    " order by convert(varchar, first_date, 23)  "
                    , category, status), "reg");
            else if (type == "thisweeksched")
                return Data_Load(String.Format(
                    " select datename(dw, first_date) as weekDay, day(first_date) as dayNbr, convert(varchar, first_date, 23) as invDate, count(distinct org + name + area + service) as tot " +
                    " from (select org, name, area, service, min(scheduled) as first_date from bis where reg_cat_id = '{0}' and status = 'Scheduled' group by org, name, area, service) as b " +
                    " group by datename(dw, first_date), day(first_date), convert(varchar, first_date, 23) " +
                    " order by convert(varchar, first_date, 23)  "
                    , category, status), "reg");
            else
                return Data_Load(String.Format(
                    " select datename(dw, first_date) as weekDay, day(first_date) as dayNbr, convert(varchar, first_date, 23) as invDate, count(distinct org + name + area + service) as tot " +
                    " from (select org, name, area, service, min(scheduled) as first_date from bis where reg_cat_id = 'Archive' and status = '{0}' and weekend = '{1}' group by org, name, area, service) as b " +
                    " group by datename(dw, first_date), day(first_date), convert(varchar, first_date, 23) " +
                    " order by convert(varchar, first_date, 23)  "
                    , status, we), "reg");
        }
        else
        {

            if (type == "thisweek")
                return Data_Load(String.Format(
                    " select datename(dw, first_date) as weekDay, day(first_date) as dayNbr, convert(varchar, first_date, 23) as invDate, count(distinct org + name + area + service) as tot " +
                    " from (select org, name, area, service, min(scheduled) as first_date from bis where reg_cat_id = '{0}' and status = '{1}' and org = '{2}' group by org, name, area, service) as b " +
                    " group by datename(dw, first_date), day(first_date), convert(varchar, first_date, 23) " +
                    " order by convert(varchar, first_date, 23)  "
                    , category, status, org), "reg");
            else if (type == "thisweeksched")
                return Data_Load(String.Format(
                    " select datename(dw, first_date) as weekDay, day(first_date) as dayNbr, convert(varchar, first_date, 23) as invDate, count(distinct org + name + area + service) as tot " +
                    " from (select org, name, area, service, min(scheduled) as first_date from bis where reg_cat_id = '{0}' and status = 'Scheduled'  and org = '{2}' group by org, name, area, service) as b " +
                    " group by datename(dw, first_date), day(first_date), convert(varchar, first_date, 23) " +
                    " order by convert(varchar, first_date, 23)  "
                    , category, status, org), "reg");
            else
                return Data_Load(String.Format(
                    " select datename(dw, first_date) as weekDay, day(first_date) as dayNbr, convert(varchar, first_date, 23) as invDate, count(distinct org + name + area + service) as tot " +
                    " from (select org, name, area, service, min(scheduled) as first_date from bis where reg_cat_id = 'Archive' and status = '{0}' and weekend = '{1}'  and org = '{2}' group by org, name, area, service) as b " +
                    " group by datename(dw, first_date), day(first_date), convert(varchar, first_date, 23) " +
                    " order by convert(varchar, first_date, 23)  "
                    , status, we, org), "reg");

        }


    }

    public DataTable GetDailyCategoryDistinct(string type, string category, string status, string we, string org)
    {
        if (org == "Combined")
        {

            if (type == "thisweek")
                return Data_Load(String.Format(
                    " select datename(dw, first_date) as weekDay, day(first_date) as dayNbr, convert(varchar, first_date, 23) as invDate, count(distinct org + name) as tot " +
                    " from (select org, name, min(scheduled) as first_date from bis where reg_cat_id = '{0}' and status = '{1}' group by org, name) as b " +
                    " group by datename(dw, first_date), day(first_date), convert(varchar, first_date, 23) " +
                    " order by convert(varchar, first_date, 23)  "
                    , category, status), "reg");
            else if (type == "thisweeksched")
                return Data_Load(String.Format(
                    " select datename(dw, first_date) as weekDay, day(first_date) as dayNbr, convert(varchar, first_date, 23) as invDate, count(distinct org + name) as tot " +
                    " from (select org, name, min(scheduled) as first_date from bis where reg_cat_id = '{0}' and status = 'Scheduled' group by org, name) as b " +
                    " group by datename(dw, first_date), day(first_date), convert(varchar, first_date, 23) " +
                    " order by convert(varchar, first_date, 23)  "
                    , category, status), "reg");
            else
                return Data_Load(String.Format(
                    " select datename(dw, first_date) as weekDay, day(first_date) as dayNbr, convert(varchar, first_date, 23) as invDate, count(distinct org + name) as tot " +
                    " from (select org, name, min(scheduled) as first_date from bis where reg_cat_id = 'Archive' and status = '{0}' and weekend = '{1}' group by org, name) as b " +
                    " group by datename(dw, first_date), day(first_date), convert(varchar, first_date, 23) " +
                    " order by convert(varchar, first_date, 23)  "
                    , status, we), "reg");
        }
        else
        {

            if (type == "thisweek")
                return Data_Load(String.Format(
                    " select datename(dw, first_date) as weekDay, day(first_date) as dayNbr, convert(varchar, first_date, 23) as invDate, count(distinct org + name) as tot " +
                    " from (select org, name, min(scheduled) as first_date from bis where reg_cat_id = '{0}' and status = '{1}' and org = '{2}' group by org, name) as b " +
                    " group by datename(dw, first_date), day(first_date), convert(varchar, first_date, 23) " +
                    " order by convert(varchar, first_date, 23)  "
                    , category, status, org), "reg");
            else if (type == "thisweeksched")
                return Data_Load(String.Format(
                    " select datename(dw, first_date) as weekDay, day(first_date) as dayNbr, convert(varchar, first_date, 23) as invDate, count(distinct org + name) as tot " +
                    " from (select org, name, min(scheduled) as first_date from bis where reg_cat_id = '{0}' and status = 'Scheduled' and org = '{2}' group by org, name) as b " +
                    " group by datename(dw, first_date), day(first_date), convert(varchar, first_date, 23) " +
                    " order by convert(varchar, first_date, 23)  "
                    , category, status, org), "reg");
            else
                return Data_Load(String.Format(
                    " select datename(dw, first_date) as weekDay, day(first_date) as dayNbr, convert(varchar, first_date, 23) as invDate, count(distinct org + name) as tot " +
                    " from (select org, name, min(scheduled) as first_date from bis where reg_cat_id = 'Archive' and status = '{0}' and org = '{2}' and weekend = '{1}' group by org, name) as b " +
                    " group by datename(dw, first_date), day(first_date), convert(varchar, first_date, 23) " +
                    " order by convert(varchar, first_date, 23)  "
                    , status, we, org), "reg");
        }

    }

    public DataTable GetOrgNonDupDailyCategory(string type, string category, string status, string org)
    {

        if (org == "Combined")
        {

            if (type == "thisweek")
                return Data_Load(String.Format(
                        "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                        "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name + area + service) AS tot FROM bis p WHERE reg_Cat_id = '{0}' " +
                        " and status = '{1}' and scheduled > getdate() - 8  " +
                        "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name + area + service) AS tot FROM bis " +
                        "WHERE reg_Cat_id = '{0}' and status = '{1}' and scheduled > getdate() - 8 " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(getdate()) GROUP BY datename(dw, scheduled), day(scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name + area + service) AS tot FROM bis " +
                        "WHERE reg_Cat_id = '{0}' and status = '{1}' and scheduled > getdate() - 8  " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) >= day(getdate()) GROUP BY datename(dw, scheduled), day(scheduled) ) d " +
                        "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", category, status), "reg");
            else if (type == "thisweeksched")
                return Data_Load(String.Format(
                        "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                        "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name + area + service) AS tot FROM bis p WHERE reg_Cat_id = '{0}' " +
                        " and status = 'Scheduled' and scheduled > getdate() - 8  " +
                        "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name + area + service) AS tot FROM bis WHERE reg_Cat_id = '{0}' " +
                        "and status = 'Scheduled' and scheduled > getdate() - 8 " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(getdate())  GROUP BY datename(dw, scheduled), day(scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name + area + service) AS tot FROM bis WHERE reg_Cat_id = '{0}' " +
                        "and status = 'Scheduled' and scheduled > getdate() - 8  " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) = day(getdate())  GROUP BY datename(dw, scheduled), day(scheduled) ) d " +
                        "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", category, status), "reg");
            return Data_Load(String.Format(

                    "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                    "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name + area + service) AS tot FROM bis p WHERE reg_Cat_id = 'Archive' and status = '{1}' and weekend > getdate() - 8  " +
                    "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                    "UNION " +
                    "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name + area + service) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and status = '{1}' and weekend > getdate() - 8 " +
                    "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(scheduled) GROUP BY datename(dw, scheduled), day(scheduled)" +
                    "UNION " +
                    "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name + area + service) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and status = '{1}' and weekend > getdate() - 8  " +
                    "and datename(dw,scheduled) = 'Thursday' and day(scheduled) = day(scheduled) GROUP BY datename(dw, scheduled), day(scheduled)) d " +
                    "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", category, status), "reg");
        }
        else
        {
            if (type == "thisweek")
                return Data_Load(String.Format(
                        "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                        "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name + area + service) AS tot FROM bis p WHERE reg_Cat_id = '{0}' " +
                        " and status = '{1}' and scheduled > getdate() - 8 and org = '{2}' " +
                        "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name + area + service) AS tot FROM bis " +
                        "WHERE reg_Cat_id = '{0}' and status = '{1}' and scheduled > getdate() - 8 and org = '{2}'" +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(getdate()) GROUP BY datename(dw, scheduled), day(scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name + area + service) AS tot FROM bis " +
                        "WHERE reg_Cat_id = '{0}' and status = '{1}' and scheduled > getdate() - 8 and org = '{2}' " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) >= day(getdate()) GROUP BY datename(dw, scheduled), day(scheduled) ) d " +
                        "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", category, status, org), "reg");
            else if (type == "thisweeksched")
                return Data_Load(String.Format(
                        "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                        "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name + area + service) AS tot FROM bis p WHERE reg_Cat_id = '{0}' " +
                        " and status = 'Scheduled' and scheduled > getdate() - 8 and org = '{2}' " +
                        "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name + area + service) AS tot FROM bis WHERE reg_Cat_id = '{0}' " +
                        "and status = 'Scheduled' and scheduled > getdate() - 8 and org = '{2}' " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(getdate())  GROUP BY datename(dw, scheduled), day(scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name + area + service) AS tot FROM bis WHERE reg_Cat_id = '{0}' " +
                        "and status = 'Scheduled' and scheduled > getdate() - 8 and org = '{2}' " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) >= day(getdate())  GROUP BY datename(dw, scheduled), day(scheduled) ) d " +
                        "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", category, status, org), "reg");
            return Data_Load(String.Format(
                    "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                    "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name + area + service) AS tot FROM bis p WHERE reg_Cat_id = 'Archive' and status = '{1}' and scheduled > getdate() - 8 and org = '{2}' " +
                    "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                    "UNION " +
                    "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name + area + service) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and status = '{1}' and scheduled > getdate() - 8 and org = '{2}' " +
                    "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(scheduled) GROUP BY datename(dw, scheduled), day(scheduled)" +
                    "UNION " +
                    "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name + area + service) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and status = '{1}' and scheduled > getdate() - 8 and org = '{2}'  " +
                    "and datename(dw,scheduled) = 'Thursday' and day(scheduled) = day(scheduled) GROUP BY datename(dw, scheduled), day(scheduled)) d " +
                    "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", category, status, org), "reg");
        }
    }


    public DataTable GetOrgDailyCategory(string type, string category, string status, string org)
    {

        if (org == "Combined")
        {

            if (type == "thisweek")
                return Data_Load(String.Format(
                        "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                        "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = '{0}' " +
                        " and status = '{1}' and scheduled > getdate() - 8  " +
                        "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis " +
                        "WHERE reg_Cat_id = '{0}' and status = '{1}' and scheduled > getdate() - 8 " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(getdate()) GROUP BY datename(dw, scheduled), day(scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis " +
                        "WHERE reg_Cat_id = '{0}' and status = '{1}' and scheduled > getdate() - 8  " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) >= day(getdate()) GROUP BY datename(dw, scheduled), day(scheduled) ) d " +
                        "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", category, status), "reg");
            else if (type == "thisweeksched")
                return Data_Load(String.Format(
                        "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                        "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = '{0}' " +
                        " and status = 'Scheduled' and scheduled > getdate() - 8  " +
                        "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = '{0}' " +
                        "and status = 'Scheduled' and scheduled > getdate() - 8 " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(getdate())  GROUP BY datename(dw, scheduled), day(scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = '{0}' " +
                        "and status = 'Scheduled' and scheduled > getdate() - 8  " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) = day(getdate())  GROUP BY datename(dw, scheduled), day(scheduled) ) d " +
                        "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", category, status), "reg");
            return Data_Load(String.Format(

                    "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                    "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'Archive' and status = '{1}' and weekend > getdate() - 8  " +
                    "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                    "UNION " +
                    "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and status = '{1}' and weekend > getdate() - 8 " +
                    "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(scheduled) GROUP BY datename(dw, scheduled), day(scheduled)" +
                    "UNION " +
                    "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and status = '{1}' and weekend > getdate() - 8  " +
                    "and datename(dw,scheduled) = 'Thursday' and day(scheduled) = day(scheduled) GROUP BY datename(dw, scheduled), day(scheduled)) d " +
                    "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", category, status), "reg");
        }
        else
        {
            if (type == "thisweek")
                return Data_Load(String.Format(
                        "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                        "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = '{0}' " +
                        " and status = '{1}' and scheduled > getdate() - 8 and org = '{2}' " +
                        "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis " +
                        "WHERE reg_Cat_id = '{0}' and status = '{1}' and scheduled > getdate() - 8 and org = '{2}'" +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(getdate()) GROUP BY datename(dw, scheduled), day(scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis " +
                        "WHERE reg_Cat_id = '{0}' and status = '{1}' and scheduled > getdate() - 8 and org = '{2}' " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) >= day(getdate()) GROUP BY datename(dw, scheduled), day(scheduled) ) d " +
                        "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", category, status, org), "reg");
            else if (type == "thisweeksched")
                return Data_Load(String.Format(
                        "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                        "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = '{0}' " +
                        " and status = 'Scheduled' and scheduled > getdate() - 8 and org = '{2}' " +
                        "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = '{0}' " +
                        "and status = 'Scheduled' and scheduled > getdate() - 8 and org = '{2}' " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(getdate())  GROUP BY datename(dw, scheduled), day(scheduled) " +
                        "UNION " +
                        "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = '{0}' " +
                        "and status = 'Scheduled' and scheduled > getdate() - 8 and org = '{2}' " +
                        "and datename(dw,scheduled) = 'Thursday' and day(scheduled) >= day(getdate())  GROUP BY datename(dw, scheduled), day(scheduled) ) d " +
                        "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", category, status, org), "reg");
            return Data_Load(String.Format(
                    "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                    "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'Archive' and status = '{1}' and scheduled > getdate() - 8 and org = '{2}' " +
                    "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                    "UNION " +
                    "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and status = '{1}' and scheduled > getdate() - 8 and org = '{2}' " +
                    "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(scheduled) GROUP BY datename(dw, scheduled), day(scheduled)" +
                    "UNION " +
                    "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and status = '{1}' and scheduled > getdate() - 8 and org = '{2}'  " +
                    "and datename(dw,scheduled) = 'Thursday' and day(scheduled) = day(scheduled) GROUP BY datename(dw, scheduled), day(scheduled)) d " +
                    "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", category, status, org), "reg");
        }
    }

    public DataTable GetDailyCompResign(string type)
    {
        if (type == "thisweek")
            return Data_Load(
                     "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                     "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'Comp Resign' " +
                    " and status = 'Comp Resign' and scheduled > getdate() - 8  " +
                     "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                     "UNION " +
                     "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Comp Resign' and status = 'Comp Resign' and scheduled > getdate() - 8 " +
                     "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(getdate())  GROUP BY datename(dw, scheduled), day(scheduled) " +
                     "UNION " +
                     "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Comp Resign' and status = 'Comp Resign' and scheduled > getdate() - 8  " +
                     "and datename(dw,scheduled) = 'Thursday' and day(scheduled) >= day(getdate())  GROUP BY datename(dw, scheduled), day(scheduled) ) d " +
                     "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", "reg");
        else if (type == "thisweeksched")
            return Data_Load(
                     "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                     "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'Comp Resign' " +
                    " and status = 'Scheduled' and scheduled > getdate() - 8  " +
                     "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                     "UNION " +
                     "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Comp Resign' " +
                    "and status = 'Scheduled' and scheduled > getdate() - 8 " +
                     "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(getdate())  GROUP BY datename(dw, scheduled), day(scheduled) " +
                     "UNION " +
                     "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Comp Resign' " +
                    "and status = 'Scheduled' and scheduled > getdate() - 8  " +
                     "and datename(dw,scheduled) = 'Thursday' and day(scheduled) >= day(getdate())  GROUP BY datename(dw, scheduled), day(scheduled) ) d " +
                     "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", "reg");
        return Data_Load(

                 "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                 "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'Archive' and status = 'Comp Resign' and scheduled > getdate() - 8  " +
                 "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                 "UNION " +
                 "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and status = 'Comp Resign' and scheduled > getdate() - 8 " +
                 "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(scheduled) GROUP BY datename(dw, scheduled), day(scheduled)" +
                 "UNION " +
                 "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and status = 'Comp Resign' and scheduled > getdate() - 8  " +
                 "and datename(dw,scheduled) = 'Thursday' and day(scheduled) = day(scheduled) GROUP BY datename(dw, scheduled), day(scheduled)) d " +
                 "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", "reg");

    }

    public DataTable GetDailyBIS(string type)
    {
        if (type == "thisweek")
            return Data_Load(
                     "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                     "SELECT datename(dw, weekend) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'LineUp' " +
                     "and status = 'In The Shop' and weekend > getdate() - 8  " +
                     "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8 GROUP BY NAME, ORG ) " +
                     "and datename(dw,weekend) <> 'Thursday' GROUP BY datename(dw, weekend) " +
                     "UNION " +
                     "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8 " +
                     "and datename(dw,weekend) = 'Thursday' and day(weekend) < day(getdate())  " +
                     "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8 GROUP BY NAME, ORG ) " +
                     "GROUP BY datename(dw, weekend), day(weekend) " +
                     "UNION " +
                     "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8 " +
                     "and datename(dw,weekend) = 'Thursday' and day(weekend) >= day(getdate())  " +
                     "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8 GROUP BY NAME, ORG ) " +
                     "GROUP BY datename(dw, weekend), day(weekend) ) d " +
                     "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3 ", "reg");
        else if (type == "thisweeksched")
            return Data_Load(
                     "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                     "SELECT datename(dw, weekend) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'LineUp' " +
                    " and status = 'Scheduled' and weekend > getdate() - 8  " +
                     "and datename(dw,weekend) <> 'Thursday' GROUP BY datename(dw, weekend) " +
                     "UNION " +
                     "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'LineUp' " +
                    "and status = 'Scheduled' and weekend > getdate() - 8 " +
                     "and datename(dw,weekend) = 'Thursday' and day(weekend) < day(getdate())  GROUP BY datename(dw, weekend), day(weekend) " +
                     "UNION " +
                     "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'LineUp' " +
                    "and status = 'Scheduled' and weekend > getdate() - 8  " +
                     "and datename(dw,weekend) = 'Thursday' and day(weekend) < day(weekend)  GROUP BY datename(dw, weekend), day(weekend) ) d " +
                     "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", "reg");
        return Data_Load(
                 "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                 "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'Archive' and weekend > getdate() - 8  " +
                 "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'Archive' and status = 'In The Shop' and weekend > getdate() - 8 GROUP BY NAME, ORG ) " +
                 "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                 "UNION " +
                 "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and weekend > getdate() - 8 " +
                 "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'Archive' and status = 'In The Shop' and weekend > getdate() - 8 GROUP BY NAME, ORG ) " +
                 "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(weekend) GROUP BY datename(dw, scheduled), day(scheduled)" +
                 "UNION " +
                 "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and weekend > getdate() - 8  " +
                 "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'Archive' and status = 'In The Shop' and weekend > getdate() - 8 GROUP BY NAME, ORG ) " +
                 "and datename(dw,scheduled) = 'Thursday' and day(scheduled) = day(weekend) GROUP BY datename(dw, scheduled), day(scheduled)) d " +
                 "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", "reg");
    }

    public DataTable GetOrgDailyBIS(string type, string org)
    {

        if (org == "Combined")
        {

            if (type == "thisweek")
                return Data_Load(
                        "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                        "SELECT datename(dw, weekend) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'LineUp' " +
                        "and status = 'In The Shop' and weekend > getdate() - 8  " +
                        "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8 GROUP BY NAME, ORG ) " +
                        "and datename(dw,weekend) <> 'Thursday' GROUP BY datename(dw, weekend) " +
                        "UNION " +
                        "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8 " +
                        "and datename(dw,weekend) = 'Thursday' and day(weekend) < day(getdate())  " +
                        "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8 GROUP BY NAME, ORG ) " +
                        "GROUP BY datename(dw, weekend), day(weekend) " +
                        "UNION " +
                        "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8 " +
                        "and datename(dw,weekend) = 'Thursday' and day(weekend) >= day(getdate())  " +
                        "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8 GROUP BY NAME, ORG ) " +
                        "GROUP BY datename(dw, weekend), day(weekend) ) d " +
                        "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3 ", "reg");
            else if (type == "thisweeksched")
                return Data_Load(
                        "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                        "SELECT datename(dw, weekend) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'LineUp' " +
                        " and status = 'Scheduled' and weekend > getdate() - 8  " +
                        "and datename(dw,weekend) <> 'Thursday' GROUP BY datename(dw, weekend) " +
                        "UNION " +
                        "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'LineUp' " +
                        "and status = 'Scheduled' and weekend > getdate() - 8 " +
                        "and datename(dw,weekend) = 'Thursday' and day(weekend) < day(getdate())  GROUP BY datename(dw, weekend), day(weekend) " +
                        "UNION " +
                        "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'LineUp' " +
                        "and status = 'Scheduled' and weekend > getdate() - 8  " +
                        "and datename(dw,weekend) = 'Thursday' and day(weekend) < day(weekend)  GROUP BY datename(dw, weekend), day(weekend) ) d " +
                        "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", "reg");
            return Data_Load(
                    "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                    "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'Archive' and weekend > getdate() - 8  " +
                    "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'Archive' and status = 'In The Shop' and weekend > getdate() - 8 GROUP BY NAME, ORG ) " +
                    "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                    "UNION " +
                    "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and weekend > getdate() - 8 " +
                    "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'Archive' and status = 'In The Shop' and weekend > getdate() - 8 GROUP BY NAME, ORG ) " +
                    "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(weekend) GROUP BY datename(dw, scheduled), day(scheduled)" +
                    "UNION " +
                    "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and weekend > getdate() - 8  " +
                    "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'Archive' and status = 'In The Shop' and weekend > getdate() - 8 GROUP BY NAME, ORG ) " +
                    "and datename(dw,scheduled) = 'Thursday' and day(scheduled) = day(weekend) GROUP BY datename(dw, scheduled), day(scheduled)) d " +
                    "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", "reg");
        }
        else
        {

            if (type == "thisweek")
                return Data_Load(String.Format(
                        "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                        "SELECT datename(dw, weekend) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'LineUp' " +
                        "and status = 'In The Shop' and weekend > getdate() - 8  " +
                        "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8  and org = '{0}' GROUP BY NAME, ORG ) " +
                        "and datename(dw,weekend) <> 'Thursday' GROUP BY datename(dw, weekend) " +
                        "UNION " +
                        "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8 " +
                        "and datename(dw,weekend) = 'Thursday' and day(weekend) < day(getdate())  " +
                        "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8  and org = '{0}' GROUP BY NAME, ORG ) " +
                        "GROUP BY datename(dw, weekend), day(weekend) " +
                        "UNION " +
                        "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8 " +
                        "and datename(dw,weekend) = 'Thursday' and day(weekend) >= day(getdate())  " +
                        "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'LineUp' and status = 'In The Shop' and weekend > getdate() - 8  and org = '{0}' GROUP BY NAME, ORG ) " +
                        "GROUP BY datename(dw, weekend), day(weekend) ) d " +
                        "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3 ", org), "reg");
            else if (type == "thisweeksched")
                return Data_Load(String.Format(
                        "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                        "SELECT datename(dw, weekend) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'LineUp' " +
                        " and status = 'Scheduled' and weekend > getdate() - 8  and org = '{0}'  " +
                        "and datename(dw,weekend) <> 'Thursday' GROUP BY datename(dw, weekend) " +
                        "UNION " +
                        "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'LineUp' " +
                        "and status = 'Scheduled' and weekend > getdate() - 8  and org = '{0}' " +
                        "and datename(dw,weekend) = 'Thursday' and day(weekend) < day(getdate())  GROUP BY datename(dw, weekend), day(weekend) " +
                        "UNION " +
                        "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'LineUp' " +
                        "and status = 'Scheduled' and weekend > getdate() - 8   and org = '{0}' " +
                        "and datename(dw,weekend) = 'Thursday' and day(weekend) < day(weekend)  GROUP BY datename(dw, weekend), day(weekend) ) d " +
                        "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", org), "reg");
            return Data_Load(String.Format(
                    "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
                    "SELECT datename(dw, scheduled) AS week_day, count(distinct org + name) AS tot FROM bis p WHERE reg_Cat_id = 'Archive' and weekend > getdate() - 8  " +
                    "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'Archive' and status = 'In The Shop' and weekend > getdate() - 8  and org = '{0}' GROUP BY NAME, ORG ) " +
                    "and datename(dw,scheduled) <> 'Thursday' GROUP BY datename(dw, scheduled) " +
                    "UNION " +
                    "SELECT 'ThursdayAfter2' as week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and weekend > getdate() - 8 " +
                    "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'Archive' and status = 'In The Shop' and weekend > getdate() - 8  and org = '{0}' GROUP BY NAME, ORG ) " +
                    "and datename(dw,scheduled) = 'Thursday' and day(scheduled) < day(weekend) GROUP BY datename(dw, scheduled), day(scheduled)" +
                    "UNION " +
                    "SELECT 'ThursdayBefore2' AS week_day, count(distinct org + name) AS tot FROM bis WHERE reg_Cat_id = 'Archive' and weekend > getdate() - 8  " +
                    "AND ID IN (SELECT MIN(ID) FROM bis p WHERE reg_Cat_id = 'Archive' and status = 'In The Shop' and weekend > getdate() - 8  and org = '{0}' GROUP BY NAME, ORG ) " +
                    "and datename(dw,scheduled) = 'Thursday' and day(scheduled) = day(weekend) GROUP BY datename(dw, scheduled), day(scheduled)) d " +
                    "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", org), "reg");

        }
    }

    public DataTable GetDailyGI(string type, string org, string we)
    {

        if (org == "Combined")
        {

            if (type == "thisweek")
                return Data_Load(String.Format(
                    "select datename(dw, appt) as weekDay, day(appt) as dayNbr, convert(varchar, appt, 23) as invDate, SUM(AMOUNT) as amt from reg " +
                    "where reg_cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') " +
                    "GROUP BY datename(dw, appt), day(appt),  convert(varchar, appt, 23) " +
                     "order by  convert(varchar, appt, 23) ", we, org), "reg");
            else if (type == "thisweekinv")
                return Data_Load(String.Format(
                    "select datename(dw, appt) as weekDay, day(appt) as dayNbr, convert(varchar, appt, 23) as invDate, SUM(AMOUNT) as amt from reg " +
                    "where reg_cat_id = 'LineUp' and status in ('GI Invoiced') " +
                    "GROUP BY datename(dw, appt), day(appt),  convert(varchar, appt, 23) " +
                    "order by convert(varchar, appt, 23)", we, org), "reg");
            else
                return Data_Load(String.Format(
                    "select datename(dw, appt) as weekDay, day(appt) as dayNbr, convert(varchar, appt, 23) as invDate, SUM(AMOUNT) as amt from reg " +
                    "where reg_cat_id = 'Archive' and status = 'GI Invoiced' and tm = '{0}'  " +
                    "GROUP BY datename(dw, appt), day(appt),  convert(varchar, appt, 23) " +
                    "order by  convert(varchar, appt, 23) ", we, org), "reg");
        }
        else
        {
            if (type == "thisweek")
                return Data_Load(String.Format(
                    "select datename(dw, appt) as weekDay, day(appt) as dayNbr, convert(varchar, appt, 23) as invDate, SUM(AMOUNT) as amt from reg " +
                    "where reg_cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') and org = '{1}'  " +
                    "GROUP BY datename(dw, appt), day(appt),  convert(varchar, appt, 23)  " +
                    "order by  convert(varchar, appt, 23) ", we, org), "reg");
            else if (type == "thisweekinv")
                return Data_Load(String.Format(
                    "select datename(dw, appt) as weekDay, day(appt) as dayNbr, convert(varchar, appt, 23) as invDate, SUM(AMOUNT) as amt from reg " +
                    "where reg_cat_id = 'LineUp' and status in ('GI Invoiced') and org = '{1}'  " +
                    "GROUP BY datename(dw, appt), day(appt),  convert(varchar, appt, 23)  " +
                    "order by  convert(varchar, appt, 23) ", we, org), "reg");
            else
                return Data_Load(String.Format(
                    "select datename(dw, appt) as weekDay, day(appt) as dayNbr, convert(varchar, appt, 23) as invDate, SUM(AMOUNT) as amt from reg " +
                    "where reg_cat_id = 'Archive' and status = 'GI Invoiced' and tm = '{0}' and org = '{1}' " +
                    "GROUP BY datename(dw, appt), day(appt),  convert(varchar, appt, 23) " +
                    "order by  convert(varchar, appt, 23) ", we, org), "reg");
        }
    }

    // public DataTable GetOrgDailyGI(string type, string org)
    // {		

    // if(org=="Combined"){

    // if(type == "thisweek")
    // return Data_Load("SELECT desc2 as week_day, h.desc1 as day_desc, COALESCE(d.tot,0) as tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
    // "SELECT datename(dw, tm) AS week_day, sum(amount) AS tot FROM reg p " +
    // "WHERE reg_Cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') and tm > getdate() - 8  " +
    // "GROUP BY datename(dw, tm) " +
    // "UNION " +
    // "SELECT 'ThursdayAfter2' as week_day, sum(amount)  AS tot FROM reg WHERE reg_Cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') and tm < getdate() - 1 " +
    // "and datename(dw,tm) = 'Thursday'  " +
    // "GROUP BY datename(dw, tm), day(tm) " +
    // "UNION " +
    // "SELECT 'ThursdayBefore2' AS week_day, sum(amount) AS tot FROM reg WHERE reg_Cat_id = 'LineUp' and status  in ('GI Invoiced', 'GI Confirmed') and tm > getdate() - 1 " +
    // "and datename(dw,tm) = 'Thursday' " +
    // "GROUP BY datename(dw, tm), day(tm) ) d " +
    // "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3 ", "reg");
    // else if(type=="thisweekinv")
    // return Data_Load("SELECT desc2 as week_day, h.desc1 as day_desc, COALESCE(d.tot,0) as tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
    // "SELECT datename(dw, tm) AS week_day, sum(amount) AS tot FROM reg p " +
    // "WHERE reg_Cat_id = 'LineUp' and status in ('GI Invoiced') and tm > getdate() - 8  " +
    // "GROUP BY datename(dw, tm) " +
    // "UNION " +
    // "SELECT 'ThursdayAfter2' as week_day, sum(amount)  AS tot FROM reg WHERE reg_Cat_id = 'LineUp' and status in ('GI Invoiced') and tm < getdate() - 1  " +
    // "and datename(dw,tm) = 'Thursday' " +
    // "GROUP BY datename(dw, tm), day(tm) " +
    // "UNION " +
    // "SELECT 'ThursdayBefore2' AS week_day, sum(amount) AS tot FROM reg WHERE reg_Cat_id = 'LineUp' and status  in ('GI Invoiced') and tm > getdate() - 1   " +
    // "and datename(dw,tm) = 'Thursday' " +
    // "GROUP BY datename(dw, tm), day(tm) ) d " +
    // "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3 ", "reg");
    // else
    // return Data_Load(

    // "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
    // "SELECT datename(dw, appt) AS week_day, sum(amount) AS tot FROM reg p WHERE reg_Cat_id = 'Archive' and tm > getdate() - 8  " +
    // "and datename(dw,appt) <> 'Thursday' and datename(dw, getdate()) <> 'Thursday' GROUP BY datename(dw, appt) " +
    // "UNION " +
    // "SELECT datename(dw, appt) AS week_day, sum(amount) AS tot FROM reg p WHERE reg_Cat_id = 'Archive' and tm > getdate() - 8  and appt >= getdate() - 7  " +
    // "and datename(dw,appt) <> 'Thursday' and datename(dw, getdate()) = 'Thursday' GROUP BY datename(dw, appt) " +
    // "UNION " +
    // "SELECT 'ThursdayAfter2' as week_day, sum(amount) AS tot FROM reg WHERE reg_Cat_id = 'Archive' and tm > getdate() - 8 " +
    // "and datename(dw,appt) = 'Thursday' and day(appt) < day(tm) GROUP BY datename(dw, appt), day(appt)" +
    // "UNION " +
    // "SELECT 'ThursdayBefore2' AS week_day, sum(amount) AS tot FROM reg WHERE reg_Cat_id = 'Archive' and tm > getdate() - 8  " +
    // "and datename(dw,appt) = 'Thursday' and day(appt) = day(tm) GROUP BY datename(dw, appt), day(appt)) d " +
    // "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", "reg");
    // } else {

    // if(type == "thisweek")
    // return Data_Load(String.Format("SELECT desc2 as week_day, h.desc1 as day_desc, COALESCE(d.tot,0) as tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
    // "SELECT datename(dw, tm) AS week_day, sum(amount) AS tot FROM reg p " +
    // "WHERE reg_Cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') and tm > getdate() - 8 and org = '{0}' " +
    // "GROUP BY datename(dw, tm) " +
    // "UNION " +
    // "SELECT 'ThursdayAfter2' as week_day, sum(amount)  AS tot FROM reg WHERE reg_Cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') and tm < getdate() - 1 " +
    // "and datename(dw,tm) = 'Thursday' and org = '{0}'  " +
    // "GROUP BY datename(dw, tm), day(tm) " +
    // "UNION " +
    // "SELECT 'ThursdayBefore2' AS week_day, sum(amount) AS tot FROM reg WHERE reg_Cat_id = 'LineUp' and status  in ('GI Invoiced', 'GI Confirmed') and tm > getdate() - 1 " +
    // "and datename(dw,tm) = 'Thursday' and org = '{0}'  " +
    // "GROUP BY datename(dw, tm), day(tm) ) d " +
    // "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3 ", org), "reg");
    // else if(type=="thisweekinv")
    // return Data_Load(String.Format("SELECT desc2 as week_day, h.desc1 as day_desc, COALESCE(d.tot,0) as tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
    // "SELECT datename(dw, tm) AS week_day, sum(amount) AS tot FROM reg p " +
    // "WHERE reg_Cat_id = 'LineUp' and status in ('GI Invoiced') and tm > getdate() - 8  and org = '{0}'  " +
    // "GROUP BY datename(dw, tm) " +
    // "UNION " +
    // "SELECT 'ThursdayAfter2' as week_day, sum(amount)  AS tot FROM reg WHERE reg_Cat_id = 'LineUp' and status in ('GI Invoiced') and tm < getdate() - 1 " +
    // "and datename(dw,tm) = 'Thursday' and org = '{0}'  " +
    // "GROUP BY datename(dw, tm), day(tm) " +
    // "UNION " +
    // "SELECT 'ThursdayBefore2' AS week_day, sum(amount) AS tot FROM reg WHERE reg_Cat_id = 'LineUp' and status  in ('GI Invoiced') and tm > getdate() - 1 " +
    // "and datename(dw,tm) = 'Thursday' and org = '{0}'  " +
    // "GROUP BY datename(dw, tm), day(tm) ) d " +
    // "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3 ", org), "reg");
    // else
    // return Data_Load(String.Format(	
    // "SELECT desc2 as week_day, h.desc1 day_desc, COALESCE(d.tot,0) AS tot, h.desc3 as seq FROM lookup h LEFT JOIN ( " +
    // "SELECT datename(dw, appt) AS week_day, sum(amount) AS tot FROM reg p WHERE reg_Cat_id = 'Archive' and tm > getdate() - 8  and org = '{0}'  " +
    // "and datename(dw,appt) <> 'Thursday' and datename(dw, getdate()) <> 'Thursday' GROUP BY datename(dw, appt) " +
    // "UNION " +
    // "SELECT datename(dw, appt) AS week_day, sum(amount) AS tot FROM reg p WHERE reg_Cat_id = 'Archive' and tm > getdate() - 8 and appt >= getdate() - 7 and org = '{0}'  " +
    // "and datename(dw,appt) <> 'Thursday' and datename(dw, getdate()) = 'Thursday' GROUP BY datename(dw, appt) " +
    // "UNION " +
    // "SELECT 'ThursdayAfter2' as week_day, sum(amount) AS tot FROM reg WHERE reg_Cat_id = 'Archive' and tm > getdate() - 8  and org = '{0}' " +
    // "and datename(dw,appt) = 'Thursday' and day(appt) < day(tm) GROUP BY datename(dw, appt), day(appt)" +
    // "UNION " +
    // "SELECT 'ThursdayBefore2' AS week_day, sum(amount) AS tot FROM reg WHERE reg_Cat_id = 'Archive' and tm > getdate() - 8  and org = '{0}'  " +
    // "and datename(dw,appt) = 'Thursday' and day(appt) = day(tm) GROUP BY datename(dw, appt), day(appt)) d " +
    // "ON d.week_day = h.desc1 WHERE h.type = 'dow' ORDER BY DESC3", org), "reg");

    // }
    // }

    public DataTable GetReg(string type, string desc)
    {
        return Data_Load(String.Format("select full_name from listReg where short_name = '{0}'", desc), "reg");
    }

    public DataTable GetOrgReg(string org, string type, string desc)
    {
        return Data_Load(String.Format("select full_name from listReg where short_name = '{0}' and post = '{1}' ", desc, org), "reg");
    }

    public DataTable GetIncomeBar(string type)
    {
        if (type == "thisweek")
        {
            return Data_Load(String.Format("select full_name, sum(amount) as tot from listReg l, reg r where reg like '%' + full_name + '%' " +
                "and reg_Cat_id = 'LineUp' " +
                "and status in ('GI Invoiced', 'GI Confirmed') " +
                "group by full_name"), "reg");
        }
        else if (type == "thisweekinv")
        {
            return Data_Load(String.Format("select full_name, sum(amount) as tot from listReg l, reg r where reg like '%' + full_name + '%' " +
                "and reg_Cat_id = 'LineUp' " +
                "and status in ('GI Invoiced') " +
                "group by full_name"), "reg");
        }
        else if (type == "lastweek")
        {
            return Data_Load(String.Format("select full_name, sum(amount) as tot from listReg l, reg r where reg like '%' + full_name + '%' " +
                "and reg_Cat_id = 'Archive' and tm > getdate() - 8 " +
                "group by full_name"), "reg");
        }
        else
        {
            return Data_Load(String.Format("select full_name, sum(amount) as tot from listReg l, reg r where reg like '%' + full_name + '%' " +
                "and reg_Cat_id = 'Archive' " +
                "group by full_name"), "reg");
        }
    }

    public DataTable GetOrgIncomeBar(string org, string type)
    {

        if (org == "Combined")
        {
            if (type == "thisweek")
            {
                return Data_Load(String.Format("select full_name, sum(amount) as tot from listReg l, reg r where reg like '%' + full_name + '%' " +
                    "and reg_Cat_id = 'LineUp' " +
                    "and status in ('GI Invoiced', 'GI Confirmed') " +
                    "group by full_name"), "reg");
            }
            else if (type == "thisweekinv")
            {
                return Data_Load(String.Format("select full_name, sum(amount) as tot from listReg l, reg r where reg like '%' + full_name + '%' " +
                    "and reg_Cat_id = 'LineUp' " +
                    "and status in ('GI Invoiced') " +
                    "group by full_name"), "reg");
            }
            else if (type == "lastweek")
            {
                return Data_Load(String.Format("select full_name, sum(amount) as tot from listReg l, reg r where reg like '%' + full_name + '%' " +
                    "and reg_Cat_id = 'Archive' and tm > getdate() - 8 " +
                    "group by full_name"), "reg");
            }
            else
            {
                return Data_Load(String.Format("select full_name, sum(amount) as tot from listReg l, reg r where reg like '%' + full_name + '%' " +
                    "and reg_Cat_id = 'Archive' " +
                    "group by full_name"), "reg");
            }
        }
        else
        {
            if (type == "thisweek")
            {
                return Data_Load(String.Format("select full_name, sum(amount) as tot from listReg l, reg r where reg like '%' + full_name + '%' " +
                    "and reg_Cat_id = 'LineUp' " +
                    "and status in ('GI Invoiced', 'GI Confirmed') " +
                    "and post = '{0}' group by full_name", org), "reg");
            }
            else if (type == "thisweekinv")
            {
                return Data_Load(String.Format("select full_name, sum(amount) as tot from listReg l, reg r where reg like '%' + full_name + '%' " +
                    "and reg_Cat_id = 'LineUp' " +
                    "and status in ('GI Invoiced') " +
                    "and post = '{0}' group by full_name", org), "reg");
            }
            else if (type == "lastweek")
            {
                return Data_Load(String.Format("select full_name, sum(amount) as tot from listReg l, reg r where reg like '%' + full_name + '%' " +
                    "and reg_Cat_id = 'Archive' and tm > getdate() - 8 " +
                    "and post = '{0}' group by full_name", org), "reg");
            }
            else
            {
                return Data_Load(String.Format("select full_name, sum(amount) as tot from listReg l, reg r where reg like '%' + full_name + '%' " +
                    "and reg_Cat_id = 'Archive' " +
                    "and post = '{0}' group by full_name", org), "reg");
            }
        }
    }

    public DataTable GetWeeklyRegIncome(string reg)
    {

        return Data_Load(String.Format("SELECT format(tm,'d MMM y') as weekend, sum(amount) as gi "
                + "FROM reg WHERE reg_Cat_id = 'archive' and reg like '%{0}%' GROUP BY tm ORDER BY tm asc", reg), "reg");

    }

    public DataTable GetIncomeByWeek(string line, string weeks = null)
    {

        if (weeks != "")
        {
            if (line == "All")
            {
                return Data_Load(String.Format("SELECT format(tm,'d MMM y') as weekend, gi FROM ( "
                        + "select TOP {0} tm, sum(amount) as gi from reg where reg_Cat_id = 'archive' group by tm ORDER BY tm DESC "
                        + ") WE_GI ORDER BY tm ASC", weeks), "reg");
            }
            else
            {
                return Data_Load(String.Format("SELECT format(tm,'d MMM y') as weekend, gi FROM ( "
                        + "select  TOP {0} tm, sum(amount) as gi from reg where reg_Cat_id = 'archive' and line = '{1}' group by tm ORDER BY tm DESC "
                        + ") WE_GI ORDER BY tm ASC", weeks, line), "reg");
            }
        }
        else
        {
            if (line != "")
            {
                return Data_Load(String.Format("SELECT format(tm,'d MMM y') as weekend, gi FROM ( "
                        + "select tm, sum(amount) as gi from reg where reg_Cat_id = 'archive' and line = '{0}' group by tm ORDER BY tm DESC "
                        + ") WE_GI ORDER BY tm ASC", line), "reg");
            }
            else
            {
                return Data_Load("SELECT format(tm,'d MMM y') as weekend, gi FROM ( "
                        + "select tm, sum(amount) as gi from reg where reg_Cat_id = 'archive' group by tm ORDER BY tm DESC "
                        + ") WE_GI ORDER BY tm ASC", "reg");
            }
        }
    }

    public DataTable GetOrgIncomeByWeek(string org, string line, string weeks = null)
    {

        if (org == "Combined")
        {

            if (weeks != "")
            {
                if (line == "All")
                {
                    return Data_Load(String.Format("SELECT format(tm,'d MMM y') as weekend, gi FROM ( "
                            + "select TOP {0} tm, sum(amount) as gi from reg where reg_Cat_id = 'archive' group by tm ORDER BY tm DESC "
                            + ") WE_GI ORDER BY tm ASC", weeks), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(tm,'d MMM y') as weekend, gi FROM ( "
                            + "select  TOP {0} tm, sum(amount) as gi from reg where reg_Cat_id = 'archive' and line = '{1}' group by tm ORDER BY tm DESC "
                            + ") WE_GI ORDER BY tm ASC", weeks, line), "reg");
                }
            }
            else
            {
                if (line != "")
                {
                    return Data_Load(String.Format("SELECT format(tm,'d MMM y') as weekend, gi FROM ( "
                            + "select tm, sum(amount) as gi from reg where reg_Cat_id = 'archive' and line = '{0}' group by tm ORDER BY tm DESC "
                            + ") WE_GI ORDER BY tm ASC", line), "reg");
                }
                else
                {
                    return Data_Load("SELECT format(tm,'d MMM y') as weekend, gi FROM ( "
                            + "select tm, sum(amount) as gi from reg where reg_Cat_id = 'archive' group by tm ORDER BY tm DESC "
                            + ") WE_GI ORDER BY tm ASC", "reg");
                }
            }
        }
        else
        {
            if (weeks != "")
            {
                if (line == "All")
                {
                    return Data_Load(String.Format("SELECT format(tm,'d MMM y') as weekend, gi FROM ( "
                            + "select TOP {0} tm, sum(amount) as gi from reg where reg_Cat_id = 'archive' and org = '{1}' group by tm ORDER BY tm DESC "
                            + ") WE_GI ORDER BY tm ASC", weeks, org), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(tm,'d MMM y') as weekend, gi FROM ( "
                            + "select  TOP {0} tm, sum(amount) as gi from reg where reg_Cat_id = 'archive' and line = '{1}' and org = '{2}' group by tm ORDER BY tm DESC "
                            + ") WE_GI ORDER BY tm ASC", weeks, line, org), "reg");
                }
            }
            else
            {
                if (line != "")
                {
                    return Data_Load(String.Format("SELECT format(tm,'d MMM y') as weekend, gi FROM ( "
                            + "select tm, sum(amount) as gi from reg where reg_Cat_id = 'archive' and line = '{0}' and org = '{1}' group by tm ORDER BY tm DESC "
                            + ") WE_GI ORDER BY tm ASC", line, org), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(tm,'d MMM y') as weekend, gi FROM ( "
                            + "select tm, sum(amount) as gi from reg where reg_Cat_id = 'archive' and org = '{0}' group by tm ORDER BY tm DESC "
                            + ") WE_GI ORDER BY tm ASC", org), "reg");
                }
            }
        }
    }

    public DataTable GetBISByWeek(string area, string weeks = null)
    {
        if (weeks != "")
        {
            if (area == "All")
            {
                return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, bis FROM ( "
                        + "select TOP {0} weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", weeks), "reg");
            }
            else
            {
                return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                        + "select  TOP {0} weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and area = '{1}' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", weeks, area), "reg");
            }
        }
        else
        {
            if (area != "")
            {
                return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                        + "select weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and area = '{0}' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", area), "reg");
            }
            else
            {
                return Data_Load("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                        + "select weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", "reg");
            }
        }
    }

    public DataTable GetOrgBISByWeek(string org, string area, string weeks = null)
    {
        if (org == "Combined")
        {

            if (weeks != "")
            {
                if (area == "All")
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, bis FROM ( "
                            + "select TOP {0} weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", weeks), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select  TOP {0} weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and area = '{1}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", weeks, area), "reg");
                }
            }
            else
            {
                if (area != "")
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and area = '{0}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", area), "reg");
                }
                else
                {
                    return Data_Load("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", "reg");
                }
            }
        }
        else
        {

            if (weeks != "")
            {
                if (area == "All")
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, bis FROM ( "
                            + "select TOP {0} weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and org = '{1}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", weeks, org), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select  TOP {0} weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and area = '{1}' and org = '{2}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", weeks, area, org), "reg");
                }
            }
            else
            {
                if (area != "")
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and area = '{0}' and org = '{1}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", area, org), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and org = '{0}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", org), "reg");
                }
            }

        }
    }

    public DataTable GetCategoryByWeek(string status, string area, string weeks = null)
    {
        if (weeks != "")
        {
            if (area == "All")
            {
                return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, bis FROM ( "
                        + "select TOP {0} weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and status = '{1}' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", weeks, status), "reg");
            }
            else
            {
                return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                        + "select  TOP {0} weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and status = '{1}' and area = '{2}' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", weeks, status, area), "reg");
            }
        }
        else
        {
            if (area != "")
            {
                return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                        + "select weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and status = '{0}' and area = '{1}' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", status, area), "reg");
            }
            else
            {
                return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                        + "select weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and status = '{0}' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", status), "reg");
            }
        }
    }

    public DataTable GetOrgCategoryByWeek(string org, string status, string area, string weeks = null)
    {
        if (org == "Combined")
        {

            if (weeks != "")
            {
                if (area == "All")
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, bis FROM ( "
                            + "select TOP {0} weekend, count(distinct org + name + area + service) as bis from bis where reg_Cat_id = 'archive' and status = '{1}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", weeks, status), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select  TOP {0} weekend, count(distinct org + name + area + service) as bis from bis where reg_Cat_id = 'archive' and status = '{1}' and area = '{2}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", weeks, status, area), "reg");
                }
            }
            else
            {
                if (area != "")
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select weekend, count(distinct org + name + area + service) as bis from bis where reg_Cat_id = 'archive' and status = '{0}' and area = '{1}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", status, area), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select weekend, count(distinct org + name + area + service) as bis from bis where reg_Cat_id = 'archive' and status = '{0}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", status), "reg");
                }
            }
        }
        else
        {
            if (weeks != "")
            {
                if (area == "All")
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, bis FROM ( "
                            + "select TOP {0} weekend, count(distinct org + name + area + service) as bis from bis where reg_Cat_id = 'archive' and status = '{1}' and org = '{2}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", weeks, status, org), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select  TOP {0} weekend, count(distinct org + name + area + service) as bis from bis where reg_Cat_id = 'archive' and status = '{1}' and area = '{2}' and org = '{3}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", weeks, status, area, org), "reg");
                }
            }
            else
            {
                if (area != "")
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select weekend, count(distinct org + name + area + service) as bis from bis where reg_Cat_id = 'archive' and status = '{0}' and area = '{1}' and org = '{2}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", status, area, org), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select weekend, count(distinct org + name + area + service) as bis from bis where reg_Cat_id = 'archive' and status = '{0}' and org = '{1}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", status, org), "reg");
                }
            }
        }
    }

    public DataTable GetWeeklyCategoryDistinct(string org, string status, string area, string weeks = null)
    {
        if (org == "Combined")
        {

            if (weeks != "")
            {
                if (area == "All")
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, bis FROM ( "
                            + "select TOP {0} weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and status = '{1}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", weeks, status), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select  TOP {0} weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and status = '{1}' and area = '{2}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", weeks, status, area), "reg");
                }
            }
            else
            {
                if (area != "")
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and status = '{0}' and area = '{1}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", status, area), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and status = '{0}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", status), "reg");
                }
            }
        }
        else
        {
            if (weeks != "")
            {
                if (area == "All")
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, bis FROM ( "
                            + "select TOP {0} weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and status = '{1}' and org = '{2}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", weeks, status, org), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select  TOP {0} weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and status = '{1}' and area = '{2}' and org = '{3}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", weeks, status, area, org), "reg");
                }
            }
            else
            {
                if (area != "")
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and status = '{0}' and area = '{1}' and org = '{2}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", status, area, org), "reg");
                }
                else
                {
                    return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, bis FROM ( "
                            + "select weekend, count(distinct org + name) as bis from bis where reg_Cat_id = 'archive' and status = '{0}' and org = '{1}' group by weekend ORDER BY weekend DESC "
                            + ") WE_GI ORDER BY weekend ASC", status, org), "reg");
                }
            }
        }
    }

    // deprecated
    public DataTable GetPHSByWeek(string weeks = null)
    {
        if (weeks != "")
        {
            return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, phs FROM ( "
                    + "select TOP {0} weekend, count(distinct org + name) as phs from bis where reg_Cat_id = 'archive' and status = 'Arrived' group by weekend ORDER BY weekend DESC "
                    + ") WE_GI ORDER BY weekend ASC", weeks), "reg");
        }
        else
        {
            return Data_Load("SELECT format(weekend,'d MMM y') as weekend, phs FROM ( "
                    + "select weekend, count(distinct org + name) as phs from bis where reg_Cat_id = 'archive' and status = 'Arrived' group by weekend ORDER BY weekend DESC "
                    + ") WE_GI ORDER BY weekend ASC", "reg");
        }
    }

    public DataTable GetOrgPHSByWeek(string org, string weeks = null)
    {
        if (org == "Combined")
        {
            if (weeks != "")
            {
                return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, phs FROM ( "
                        + "select TOP {0} weekend, count(distinct org + name) as phs from bis where reg_Cat_id = 'archive' and status = 'Arrived' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", weeks), "reg");
            }
            else
            {
                return Data_Load("SELECT format(weekend,'d MMM y') as weekend, phs FROM ( "
                        + "select weekend, count(distinct org + name) as phs from bis where reg_Cat_id = 'archive' and status = 'Arrived' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", "reg");
            }
        }
        else
        {
            if (weeks != "")
            {
                return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, phs FROM ( "
                        + "select TOP {0} weekend, count(distinct org + name) as phs from bis where reg_Cat_id = 'archive' and status = 'Arrived' and org = '{1}' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", weeks, org), "reg");
            }
            else
            {
                return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, phs FROM ( "
                        + "select weekend, count(distinct org + name) as phs from bis where reg_Cat_id = 'archive' and status = 'Arrived' and org = '{0}' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", org), "reg");
            }
        }
    }

    // deprecated
    public DataTable GetSignedByWeek(string weeks = null)
    {
        if (weeks != "")
        {
            return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, signed FROM ( "
                    + "select TOP {0} weekend, count(distinct org + name) as signed from bis where reg_Cat_id = 'Recruit' and status = 'Signed' group by weekend ORDER BY weekend DESC "
                    + ") WE_GI ORDER BY weekend ASC", weeks), "reg");
        }
        else
        {
            return Data_Load("SELECT format(weekend,'d MMM y') as weekend, signed FROM ( "
                    + "select weekend, count(distinct org + name) as signed from bis where reg_Cat_id = 'Recruit' and status = 'Signed' group by weekend ORDER BY weekend DESC "
                    + ") WE_GI ORDER BY weekend ASC", "reg");
        }
    }

    public DataTable GetOrgSignedByWeek(string org, string weeks = null)
    {
        if (org == "Combined")
        {
            if (weeks != "")
            {
                return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, signed FROM ( "
                        + "select TOP {0} weekend, count(distinct org + name) as signed from bis where reg_Cat_id = 'Recruit' and status = 'Signed' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", weeks), "reg");
            }
            else
            {
                return Data_Load("SELECT format(weekend,'d MMM y') as weekend, signed FROM ( "
                        + "select weekend, count(distinct org + name) as signed from bis where reg_Cat_id = 'Recruit' and status = 'Signed' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", "reg");
            }
        }
        else
        {
            if (weeks != "")
            {
                return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as we, signed FROM ( "
                        + "select TOP {0} weekend, count(distinct org + name) as signed from bis where reg_Cat_id = 'Recruit' and status = 'Signed' and org = '{1}' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", weeks, org), "reg");
            }
            else
            {
                return Data_Load(String.Format("SELECT format(weekend,'d MMM y') as weekend, signed FROM ( "
                        + "select weekend, count(distinct org + name) as signed from bis where reg_Cat_id = 'Recruit' and status = 'Signed' and org = '{0}' group by weekend ORDER BY weekend DESC "
                        + ") WE_GI ORDER BY weekend ASC", org), "reg");
            }
        }
    }

    public DataTable GetOrgProcurementPie(string org, string type, string fromWE = null, string toWE = null)
    {
        if (org == "Combined")
        {
            if (type == "thisweek")
            {
                return Data_Load(String.Format("select line, sum(amount) as sales from reg where reg_Cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') and line is not null group by line"), "reg");
            }
            else if (type == "lastweek")
            {
                return Data_Load(String.Format("select line, sum(amount) as sales from reg where reg_Cat_id = 'archive' and line is not null and tm > getdate() - 8 group by line"), "reg");
            }
            else
            {
                return Data_Load(String.Format("select line, sum(amount) as sales from reg where reg_Cat_id = 'archive' and line is not null group by line"), "reg");
            }
        }
        else
        {
            if (type == "thisweek")
            {
                return Data_Load(String.Format("select line, sum(amount) as sales from reg where reg_Cat_id = 'LineUp' and org = '{0}' and status in ('GI Invoiced', 'GI Confirmed') and line is not null group by line", org), "reg");
            }
            else if (type == "lastweek")
            {
                return Data_Load(String.Format("select line, sum(amount) as sales from reg where reg_Cat_id = 'archive' and org = '{0}' and line is not null and tm > getdate() - 8 group by line", org), "reg");
            }
            else
            {
                return Data_Load(String.Format("select line, sum(amount) as sales from reg where reg_Cat_id = 'archive' and org = '{0}' and line is not null group by line", org), "reg");
            }
        }
    }

    public DataTable GetProcurementPie(string type, string fromWE = null, string toWE = null)
    {
        if (type == "thisweek")
        {
            return Data_Load(String.Format("select line, sum(amount) as sales from reg where reg_Cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') and line is not null group by line"), "reg");
        }
        else if (type == "lastweek")
        {
            return Data_Load(String.Format("select line, sum(amount) as sales from reg where reg_Cat_id = 'archive' and line is not null and tm > getdate() - 8 group by line"), "reg");
        }
        else
        {
            return Data_Load(String.Format("select line, sum(amount) as sales from reg where reg_Cat_id = 'archive' and line is not null group by line"), "reg");
        }

    }

    public DataTable GetProcurementPieReg(string reg, string type = null)
    {
        if (type == "thisweek")
        {
            return Data_Load(String.Format("select line, sum(amount) as sales from reg where reg_Cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') and line is not null and reg like '%{0}%' group by line", reg), "reg");
        }
        else if (type == "lastweek")
        {
            return Data_Load(String.Format("select line, sum(amount) as sales from reg where reg_Cat_id = 'archive' and reg like '%{0}%' and line is not null and tm > getdate() - 8 group by line", reg), "reg");
        }
        else
        {
            return Data_Load(String.Format("select line, sum(amount) as sales from reg where reg_Cat_id = 'archive' and reg like '%{0}%' and line is not null group by line", reg), "reg");
        }

    }

    public DataTable GetBISPie(string type, string fromWE = null, string toWE = null)
    {
        if (type == "thisweek")
        {
            return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'LineUp' and status = 'In The Shop' group by area"), "reg");
        }
        else if (type == "lastweek")
        {
            return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'archive' and status = 'In The Shop' and weekend > getdate() - 8 group by area"), "reg");
        }
        else
        {
            return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'archive' and status = 'In The Shop' group by area"), "reg");
        }

    }

    public DataTable GetOrgBISPie(string org, string type, string fromWE = null, string toWE = null)
    {

        if (org == "Combined")
        {
            if (type == "thisweek")
            {
                return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'LineUp' and status = 'In The Shop' group by area"), "reg");
            }
            else if (type == "lastweek")
            {
                return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'archive' and status = 'In The Shop' and weekend > getdate() - 8 group by area"), "reg");
            }
            else
            {
                return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'archive' and status = 'In The Shop' group by area"), "reg");
            }
        }
        else
        {
            if (type == "thisweek")
            {
                return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'LineUp' and status = 'In The Shop' and org = '{0}' group by area", org), "reg");
            }
            else if (type == "lastweek")
            {
                return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'archive' and status = 'In The Shop' and weekend > getdate() - 8 and org = '{0}' group by area", org), "reg");
            }
            else
            {
                return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'archive' and status = 'In The Shop' and org = '{0}' group by area", org), "reg");
            }
        }
    }

    public DataTable GetCategoryPie(string type, string category, string status)
    {
        if (type == "thisweek")
        {
            return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = '{0}' and status = '{1}' group by area", category, status), "reg");
        }
        else if (type == "lastweek")
        {
            return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'archive' and status = '{1}' and weekend > getdate() - 8 group by area", category, status), "reg");
        }
        else
        {
            return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'archive' and status = '{1}' group by area", category, status), "reg");
        }

    }

    public DataTable GetOrgCategoryPie(string org, string type, string category, string status)
    {
        if (org == "Combined")
        {
            if (type == "thisweek")
            {
                return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = '{0}' and status = '{1}' group by area", category, status), "reg");
            }
            else if (type == "lastweek")
            {
                return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'archive' and status = '{1}' and weekend > getdate() - 8 group by area", category, status), "reg");
            }
            else
            {
                return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'archive' and status = '{1}' group by area", category, status), "reg");
            }
        }
        else
        {
            if (type == "thisweek")
            {
                return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = '{0}' and status = '{1}' and org = '{2}' group by area", category, status, org), "reg");
            }
            else if (type == "lastweek")
            {
                return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'archive' and status = '{1}' and weekend > getdate() - 8  and org = '{2}' group by area", category, status, org), "reg");
            }
            else
            {
                return Data_Load(String.Format("select area, count(1) as sales from bis where reg_Cat_id = 'archive' and status = '{1}' and org = '{2}'  group by area", category, status, org), "reg");
            }
        }
    }

    public DataTable GetOrgCategoryPieLine(string org, string type, string category, string status)
    {
        if (org == "Combined")
        {
            if (type == "thisweek")
            {
                return Data_Load(String.Format("select line, count(1) as sales from bis where reg_Cat_id = '{0}' and status = '{1}' group by line", category, status), "reg");
            }
            else if (type == "lastweek")
            {
                return Data_Load(String.Format("select line, count(1) as sales from bis where reg_Cat_id = 'archive' and status = '{1}' and weekend > getdate() - 8 group by line", category, status), "reg");
            }
            else
            {
                return Data_Load(String.Format("select line, count(1) as sales from bis where reg_Cat_id = 'archive' and status = '{1}' group by line", category, status), "reg");
            }
        }
        else
        {
            if (type == "thisweek")
            {
                return Data_Load(String.Format("select line, count(1) as sales from bis where reg_Cat_id = '{0}' and status = '{1}' and org = '{2}' group by line", category, status, org), "reg");
            }
            else if (type == "lastweek")
            {
                return Data_Load(String.Format("select line, count(1) as sales from bis where reg_Cat_id = 'archive' and status = '{1}' and weekend > getdate() - 8  and org = '{2}' group by line", category, status, org), "reg");
            }
            else
            {
                return Data_Load(String.Format("select line, count(1) as sales from bis where reg_Cat_id = 'archive' and status = '{1}' and org = '{2}'  group by line", category, status, org), "reg");
            }
        }
    }

    public DataTable GetOrgEventsBar2(string org, string status)
    {
        if (org == "Combined")
        {
            return Data_Load(String.Format("select service as area, count(1) as sales from bis where reg_Cat_id = 'event' and status in ('Confirmed', 'Reconfirmed') group by service", status), "reg");
        }
        else
        {
            return Data_Load(String.Format("select service as area, count(1) as sales from bis where reg_Cat_id = 'event' and status in ('Confirmed', 'Reconfirmed') and org = '{0}' group by service", org), "reg");
        }
    }

    public DataTable GetStartsBar(string org, string type)
    {
        if (org == "Combined")
        {
            return Data_Load(String.Format("select area, count(1) AS sales from bis where reg_cat_id = 'Paid Start'and status IN ('Started') and rank = '{1}' group by area", org, type), "reg");
        }
        else
        {
            return Data_Load(String.Format("select area, count(1) AS sales from bis where reg_cat_id = 'Paid Start'and status IN ('Started') and rank = '{1}' and org = '{0}' group by area", org, type), "reg");
        }
    }

    public DataTable GetDeliveryBar(string org, string area)
    {

        if (org == "Combined")
        {

            if (area == "GAK")
            {

                return Data_Load(String.Format(
                    "SELECT starts.area, sales, starts, bis, comps FROM ( " +

                    " select 'DONE' as area, count(1) AS starts from bis where reg_cat_id = 'Paid Start' and status = 'Started' and area = '{1}' " +
                    " UNION " +
                    " select 'NAMED' as area, count(1) AS starts from bis where reg_cat_id = 'Paid Start' and status in ('Named', 'Scheduled') and area = '{1}' " +

                    " ) AS starts, ( " +

                    " select 'DONE' as area, count(1) AS sales from reg where reg_cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') and rank = '{1}' " +
                    " union " +
                    " select 'NAMED' as area, count(1) AS sales from reg where reg_cat_id = 'LineUp' and status in ('Possible', 'Definite') and rank = '{1}' " +

                    " ) as sales, ( " +

                    " select 'DONE' as area, count(1) AS bis from bis where reg_cat_id = 'LineUp' and status = 'In The Shop' and area = 'KNOW' " +
                    " UNION " +
                    " select 'NAMED' as area, count(1) AS bis from bis where reg_cat_id = 'LineUp' and status in ('Scheduled') and area = 'KNOW' " +

                    " ) as bis , ( " +

                    " select 'DONE' as area, count(1) AS comps from bis where reg_cat_id = 'Comp Resign' and status = 'Comp Resign' and area = '{1}' " +
                    " UNION " +
                    " select 'NAMED' as area, count(1) AS comps from bis where reg_cat_id = 'Comp Resign' and status in ('Named', 'Scheduled') and area = '{1}' " +

                    " ) as comps " +

                    "where starts.area = sales.area and starts.area = bis.area and starts.area = comps.area order by area desc  "

                    , org, area), "reg");

            }
            else
            {

                return Data_Load(String.Format(
                    "SELECT starts.area, sales, starts, bis, comps FROM ( " +

                    " select 'DONE' as area, count(1) AS starts from bis where reg_cat_id = 'Paid Start' and status = 'Started' and area = '{1}' " +
                    " UNION " +
                    " select 'NAMED' as area, count(1) AS starts from bis where reg_cat_id = 'Paid Start' and status in ('Named', 'Scheduled') and area = '{1}' " +

                    " ) AS starts, ( " +

                    " select 'DONE' as area, count(1) AS sales from reg where reg_cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') and rank = '{1}' " +
                    " union " +
                    " select 'NAMED' as area, count(1) AS sales from reg where reg_cat_id = 'LineUp' and status in ('Possible', 'Definite') and rank = '{1}' " +

                    " ) as sales, ( " +

                    " select 'DONE' as area, count(1) AS bis from bis where reg_cat_id = 'LineUp' and status = 'In The Shop' and area = '{1}' " +
                    " UNION " +
                    " select 'NAMED' as area, count(1) AS bis from bis where reg_cat_id = 'LineUp' and status in ('Scheduled') and area = '{1}' " +

                    " ) as bis , ( " +

                    " select 'DONE' as area, count(1) AS comps from bis where reg_cat_id = 'Comp Resign' and status = 'Comp Resign' and area = '{1}' " +
                    " UNION " +
                    " select 'NAMED' as area, count(1) AS comps from bis where reg_cat_id = 'Comp Resign' and status in ('Named', 'Scheduled') and area = '{1}' " +

                    " ) as comps " +

                    "where starts.area = sales.area and starts.area = bis.area and starts.area = comps.area order by area desc  "

                    , org, area), "reg");
            }

        }
        else
        {

            if (area == "GAK")
            {

                return Data_Load(String.Format(
                    "SELECT starts.area, sales, starts, bis, comps FROM ( " +

                    " select 'DONE' as area, count(1) AS starts from bis where reg_cat_id = 'Paid Start' and status = 'Started' and org = '{0}'  and area = '{1}' " +
                    " UNION " +
                    " select 'NAMED' as area, count(1) AS starts from bis where reg_cat_id = 'Paid Start' and status in ('Named', 'Scheduled') and org = '{0}'  and area = '{1}' " +

                    " ) AS starts, ( " +

                    " select 'DONE' as area, count(1) AS sales from reg where reg_cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') and org = '{0}' and rank = '{1}' " +
                    " union " +
                    " select 'NAMED' as area, count(1) AS sales from reg where reg_cat_id = 'LineUp' and status in ('Possible', 'Definite') and org = '{0}' and rank = '{1}' " +

                    " ) as sales, ( " +

                    " select 'DONE' as area, count(1) AS bis from bis where reg_cat_id = 'LineUp' and status = 'In The Shop' and org = '{0}' and area = 'KNOW' " +
                    " UNION " +
                    " select 'NAMED' as area, count(1) AS bis from bis where reg_cat_id = 'LineUp' and status in ('Scheduled') and org = '{0}' and area = 'KNOW' " +

                    " ) as bis , ( " +

                    " select 'DONE' as area, count(1) AS comps from bis where reg_cat_id = 'Comp Resign' and status = 'Comp Resign' and org = '{0}' and area = '{1}' " +
                    " UNION " +
                    " select 'NAMED' as area, count(1) AS comps from bis where reg_cat_id = 'Comp Resign' and status in ('Named', 'Scheduled') and org = '{0}' and area = '{1}' " +

                    " ) as comps " +

                    "where starts.area = sales.area and starts.area = bis.area and starts.area = comps.area order by area desc  "

                    , org, area), "reg");

            }
            else
            {

                return Data_Load(String.Format(
                    "SELECT starts.area, sales, starts, bis, comps FROM ( " +

                    " select 'DONE' as area, count(1) AS starts from bis where reg_cat_id = 'Paid Start' and status = 'Started' and org = '{0}' and area = '{1}' " +
                    " UNION " +
                    " select 'NAMED' as area, count(1) AS starts from bis where reg_cat_id = 'Paid Start' and status in ('Named', 'Scheduled') and org = '{0}' and area = '{1}' " +

                    " ) AS starts, ( " +

                    " select 'DONE' as area, count(1) AS sales from reg where reg_cat_id = 'LineUp' and status in ('GI Invoiced', 'GI Confirmed') and org = '{0}' and rank = '{1}' " +
                    " union " +
                    " select 'NAMED' as area, count(1) AS sales from reg where reg_cat_id = 'LineUp' and status in ('Possible', 'Definite') and org = '{0}' and rank = '{1}' " +

                    " ) as sales, ( " +

                    " select 'DONE' as area, count(1) AS bis from bis where reg_cat_id = 'LineUp' and status = 'In The Shop' and org = '{0}' and area = '{1}' " +
                    " UNION " +
                    " select 'NAMED' as area, count(1) AS bis from bis where reg_cat_id = 'LineUp' and status in ('Scheduled') and org = '{0}' and area = '{1}' " +

                    " ) as bis , ( " +

                    " select 'DONE' as area, count(1) AS comps from bis where reg_cat_id = 'Comp Resign' and status = 'Comp Resign' and org = '{0}' and area = '{1}' " +
                    " UNION " +
                    " select 'NAMED' as area, count(1) AS comps from bis where reg_cat_id = 'Comp Resign' and status in ('Named', 'Scheduled') and org = '{0}' and area = '{1}' " +

                    " ) as comps " +

                    "where starts.area = sales.area and starts.area = bis.area and starts.area = comps.area order by area desc  "

                    , org, area), "reg");
            }
        }
    }

    public DataTable GetOrgEventsBar(string org, string status)
    {
        if (org == "Combined")
        {
            if (status == "Confirmed")
                return Data_Load(String.Format("select service as area, count(1) as sales from bis where reg_Cat_id = 'event' and status in ('Confirmed', 'Reconfirmed') group by service", status), "reg");
            else
                return Data_Load(String.Format("select service as area, count(1) as sales from bis where reg_Cat_id = 'event' and status = '{0}' group by service", status), "reg");
        }
        else
        {
            if (status == "Confirmed")
                return Data_Load(String.Format("select service as area, count(1) as sales from bis where reg_Cat_id = 'event' and status in ('Confirmed', 'Reconfirmed') and org = '{0}' group by service", org), "reg");
            else
                return Data_Load(String.Format("select service as area, count(1) as sales from bis where reg_Cat_id = 'event' and status = '{1}' and org = '{0}' group by service", org, status), "reg");
        }
    }

    public DateTime GetNextWeekday(DateTime start, DayOfWeek day)
    {

        TimeSpan twopm = new TimeSpan(14, 0, 0); //2 o'clock
        TimeSpan now = start.TimeOfDay;
        int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;

        if (start.DayOfWeek != DayOfWeek.Thursday)
        {
            return start.AddDays(daysToAdd);
        }
        else if (now > twopm)
        {
            return start.AddDays(7);
        }
        else
        {
            return start;
        }

    }

    public DateTime GetNextThursday(DateTime time)
    {

        TimeSpan twopm = new TimeSpan(14, 0, 0); //2 o'clock
        TimeSpan now = time.TimeOfDay;

        if (time.DayOfWeek != DayOfWeek.Thursday)
        {
            return time.Subtract(new TimeSpan((int)time.DayOfWeek - 4, 0, 0, 0));
        }
        else if (now > twopm)
        {
            return time.AddDays(7);
        }
        else
        {
            return time;
        }

    }

    private DataTable Data_Load(string command, string type)
    {
        DataTable dataTable = new DataTable();
        try
        {
            using (SqlConnection conn = databaseConnection.CreateSqlConnection())
            {
                conn.Open();
                using (SqlCommand Cmd = new SqlCommand(command, conn))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(Cmd))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                }
                conn.Close();
            }
            return dataTable;
        }

        catch
        {
            return null;
        }
    }

    static private string GetConnectionString(string type)
    {
        if (type == "reg")
        {
            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["reg"].ConnectionString;
            return connStr;
        }
        else if (type == "cf")
        {
            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["cf"].ConnectionString;
            return connStr;
        }
        return null;
    }

}