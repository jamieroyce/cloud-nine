using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;

public class pdal
{

    public DataTable getPreclears(string org)
    {
        if (org == "Combined")
        {
            return Data_Load(String.Format("select * from pc"), "reg");
        }
        else
        {
            return Data_Load(String.Format("select * from pc where org = '{0}'", org), "reg");
        }
    }

    public DataTable getPreclear(string pc)
    {
        return Data_Load(String.Format("select * from pc where name = '{0}' ", pc), "reg");
    }

    public DataTable getPreclearFromId(int pcid)
    {
        return Data_Load(String.Format("select * from pc where id = '{0}' ", pcid), "reg");
    }

    public DataTable getAuditors(string org)
    {
        if (org == "Combined")
        {
            return Data_Load(String.Format("select * from pc_auditor"), "reg");
        }
        else
        {
            return Data_Load(String.Format("select * from pc_auditor where org = '{0}'", org), "reg");
        }
    }

    public DataTable getAuditorFromId(int id)
    {
        return Data_Load(String.Format("select * from pc_auditor where id = '{0}' ", id), "reg");
    }

    public DataTable getSessions(string org)
    {
        if (org == "Combined")
        {
            return Data_Load(String.Format("select * from pc_session"), "reg");
        }
        else
        {
            return Data_Load(String.Format("select * from pc_session where org = '{0}'", org), "reg");
        }
    }

    public DataTable getDailySession(string org, string ssnday)
    {
        if (org == "Combined")
        {
            return Data_Load(String.Format(
                "select s.org, name, type, session_minutes, admin_minutes, SUM(session_minutes) + sum(admin_minutes) as minutes " +
                "from pc_session s, pc  " +
                "where pc.id = s.pc_id and session_date = '{0}' " +
                "group by s.org, name, type, session_minutes, admin_minutes ", ssnday), "reg");
        }
        else
        {
            return Data_Load(String.Format(
                "select s.org, name, type, session_minutes, admin_minutes, SUM(session_minutes) + sum(admin_minutes) as minutes " +
                "from pc_session s, pc  " +
                "where pc.id = s.pc_id and session_date = '{0}' and s.org = '{1}' " +
                "group by s.org, name, type, session_minutes, admin_minutes ", ssnday, org), "reg");
        }
    }

    public DataTable getIntensives(string pcid, string org)
    {
        if (org == "Combined")
        {
            return Data_Load(String.Format("select id, pc_id, inv_nbr, vsd_value, vsd_counted, intensive_minutes, status, date_started, we_started, we_completed, note, org, addo_id, org_id, date_created, date_modified, (inv_nbr + ' - ' + convert(varchar(10), vsd_value) + ' - ' + status) as int_desc from pc_intensives where pc_id = '{0}'", pcid), "reg");
        }
        else
        {
            return Data_Load(String.Format("select id, pc_id, inv_nbr, vsd_value, vsd_counted, intensive_minutes, status, date_started, we_started, we_completed, note, org, addo_id, org_id, date_created, date_modified, (inv_nbr + ' - ' + convert(varchar(10), vsd_value) + ' - ' + status) as int_desc from pc_intensives where pc_id = '{0}' and org = '{1}'", pcid, org), "reg");
        }
    }

    public DataTable getAccount(string addoid, string orgid = null)
    {
        return Data_Load(String.Format("select item.desc1 + ' ' + item.desc2 as item, quantity, fppp.amt_paid, fppp.amt_to_fp, fppp.fp, fppp.create_time, addo.first_name + ' ' + addo.last_name as name, addo.addo_id, fppp.org_id "
            + "from dbo.pers_fppp as fppp INNER JOIN pers_addo as addo ON fppp.pers_id = addo.pers_id "
            + "INNER JOIN item_ref as item on fppp.item_id = item.item_id "
            + "where item_type = 1 and addo.addo_id = '{0}' "
            + "order by create_time desc", addoid), "cf");
    }

    public DataTable getTE(string addoid)
    {
        return Data_Load(String.Format("select * from pc_tech_estimate where addo_id = '{0}' ", addoid), "reg");
    }

    public DataTable getInvoices(string addoid, string orgid = null)
    {

        CultureInfo currentUI = Thread.CurrentThread.CurrentUICulture;

        if (orgid != null)
        {

            if (currentUI.ToString() == "de")
            {
                return Data_Load(String.Format("select i.inv_nbr, item.desc1 + ' ' + item.desc2 as item, i.inv_total, inv.qty, inv.unit_price, fsm_name, inv_date, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id from dbo.pers_inv_detail as inv INNER JOIN pers_invoice as i on i.pers_inv_id = inv.pers_inv_id "
                    + "INNER JOIN pers_addo as addo ON inv.addo_id = addo.addo_id "
                    + "INNER JOIN item_ref as item on inv.master_item_id = item.item_id "
                    + "WHERE item.item_type = 1 and i.inv_type in ('GUTSCHRIFT', 'CASH', 'VORAUSZAHL.') and addo.last_name > '' and addo.first_name > '' and i.inv_total > 0 and addo.addo_id = '{0}' order by inv_date desc", addoid, orgid), "cf");
            }
            else
            {
                return Data_Load(String.Format("select i.inv_nbr, item.desc1 + ' ' + item.desc2 as item, i.inv_total, inv.qty, inv.unit_price, fsm_name, inv_date, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id from dbo.pers_inv_detail as inv INNER JOIN pers_invoice as i on i.pers_inv_id = inv.pers_inv_id "
                    + "INNER JOIN pers_addo as addo ON inv.addo_id = addo.addo_id "
                    + "INNER JOIN item_ref as item on inv.master_item_id = item.item_id "
                    + "WHERE item.item_type = 1 and i.inv_type in ('DONATION', 'CASH', 'CREDIT') and addo.last_name > '' and addo.first_name > '' and i.inv_total > 0 and addo.addo_id = '{0}' order by inv_date desc", addoid, orgid), "cf");
            };
        }
        else
        {

            if (currentUI.ToString() == "de")
            {
                return Data_Load(String.Format("select i.inv_nbr, item.desc1 + ' ' + item.desc2 as item, i.inv_total, inv.qty, inv.unit_price, fsm_name, inv_date, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id from dbo.pers_inv_detail as inv INNER JOIN pers_invoice as i on i.pers_inv_id = inv.pers_inv_id "
                    + "INNER JOIN pers_addo as addo ON inv.addo_id = addo.addo_id "
                    + "INNER JOIN item_ref as item on inv.master_item_id = item.item_id "
                    + "WHERE item.item_type = 1 and i.inv_type in ('GUTSCHRIFT', 'CASH', 'VORAUSZAHL.') and addo.last_name > '' and addo.first_name > '' and i.inv_total > 0 and addo.addo_id = '{0}' order by inv_date desc", addoid), "cf");
            }
            else
            {
                return Data_Load(String.Format("select i.inv_nbr, item.desc1 + ' ' + item.desc2 as item, i.inv_total, inv.qty, inv.unit_price, fsm_name, inv_date, addo.home_phone as phone, addo.first_name + ' ' "
                    + "+ addo.last_name as name, addo.addo_id from dbo.pers_inv_detail as inv INNER JOIN pers_invoice as i on i.pers_inv_id = inv.pers_inv_id "
                    + "INNER JOIN pers_addo as addo ON inv.addo_id = addo.addo_id "
                    + "INNER JOIN item_ref as item on inv.master_item_id = item.item_id "
                    + "WHERE item.item_type = 1 and i.inv_type in ('DONATION', 'CASH', 'CREDIT') and addo.last_name > '' and addo.first_name > '' and i.inv_total > 0 and addo.addo_id = '{0}' order by inv_date desc", addoid), "cf");
            };

        }
    }

    public DataTable getAuditorView(string org, string we)
    {
        if (org == "Combined")
        {
            return Data_Load(String.Format(
                 " select a.id, a.name, sum(session_minutes) as tot_ssn_min, sum(admin_minutes) as tot_admin_min, SUM(SESSION_MINUTES) + SUM(ADMIN_MINUTES) AS tot_min " +
                 " from pc_session p, pc_auditor a " +
                 " where p.auditor_id = a.id and p.weekend = '{0}' " +
                 " group by a.id, a.name "
            , we), "reg");
        }
        else
        {
            return Data_Load(String.Format(
                 " select a.id, a.name, sum(session_minutes) as tot_ssn_min, sum(admin_minutes) as tot_admin_min, SUM(SESSION_MINUTES) + SUM(ADMIN_MINUTES) AS tot_min " +
                 " from pc_session p, pc_auditor a " +
                 " where p.auditor_id = a.id and p.org = '{1}' and p.weekend = '{0}' " +
                 " group by a.id, a.name "
            , we, org), "reg");
        }
    }

    public DataTable getPcView(string org, string we)
    {
        if (org == "Combined")
        {
            return Data_Load(String.Format(
                "select vsd.pc_id, vsd.name, vsd.org, vsd.id as intensive_id, vsd.status, vsd_value, minutes_used as intensive_min_used, minutes_to_vsd, minutes_this_week, minutes_to_inco, inco.weekend " +
                "from ( " +
                "select i.pc_id, pc.name, i.org, i.id, i.status, vsd_value, i.intensive_minutes, SUM(SESSION_MINUTES) + SUM(ADMIN_MINUTES) AS minutes_used, i.intensive_minutes - (SUM(SESSION_MINUTES) + SUM(ADMIN_MINUTES)) as minutes_to_vsd " +
                "from pc_session s, pc_intensives i, pc  " +
                "where s.pc_id = i.pc_id and s.intensive_id = i.id and pc.id = s.pc_id " +
                "and i.status = 'IP' and weekend = '{0}'  " +
                "GROUP BY i.org, i.pc_id, pc.name, i.id, i.status, vsd_value, i.intensive_minutes ) as vsd,  " +
                "( select i.pc_id, i.org, WEEKEND, SUM(SESSION_MINUTES) + SUM(ADMIN_MINUTES) AS minutes_this_week, 750 - (SUM(SESSION_MINUTES) + SUM(ADMIN_MINUTES)) AS minutes_to_inco  " +
                "from pc_session s, pc_intensives i " +
                "where s.pc_id = i.pc_id and s.intensive_id = i.id " +
                "and i.status in ('IP', 'USED') " +
                "and weekend = '{0}'  " +
                "GROUP BY i.org, i.pc_id, WEEKEND ) as inco " +
                "where vsd.pc_id = inco.pc_id "
            , we), "reg");
        }
        else
        {
            return Data_Load(String.Format(
                "select vsd.pc_id, vsd.name, vsd.org, vsd.id as intensive_id, vsd.status, vsd_value, minutes_used as intensive_min_used, minutes_to_vsd, minutes_this_week, minutes_to_inco, inco.weekend " +
                "from ( " +
                "select i.pc_id, pc.name, i.org, i.id, i.status, vsd_value, i.intensive_minutes, SUM(SESSION_MINUTES) + SUM(ADMIN_MINUTES) AS minutes_used, i.intensive_minutes - (SUM(SESSION_MINUTES) + SUM(ADMIN_MINUTES)) as minutes_to_vsd " +
                "from pc_session s, pc_intensives i, pc  " +
                "where s.pc_id = i.pc_id and s.intensive_id = i.id and pc.id = s.pc_id " +
                "and i.status = 'IP' and weekend = '{0}'  " +
                "GROUP BY i.org, i.pc_id, pc.name, i.id, i.status, vsd_value, i.intensive_minutes ) as vsd,  " +
                "( select i.pc_id, i.org, WEEKEND, SUM(SESSION_MINUTES) + SUM(ADMIN_MINUTES) AS minutes_this_week, 750 - (SUM(SESSION_MINUTES) + SUM(ADMIN_MINUTES)) AS minutes_to_inco  " +
                "from pc_session s, pc_intensives i " +
                "where s.pc_id = i.pc_id and s.intensive_id = i.id " +
                "and i.status in ('IP', 'USED') " +
                "and weekend = '{0}'  " +
                "and i.org = '{1}'  " +
                "GROUP BY i.org, i.pc_id, WEEKEND ) as inco " +
                "where vsd.pc_id = inco.pc_id "
            , we, org), "reg");
        }
    }

    public string GetHoursAndMinutes(int minutes)
    {
        int Minute = 0;
        int Hour = 0;
        string formattedHours = "";

        {
            Hour = minutes / 60;
            Minute = minutes % 60;
            formattedHours = FormatTwoDigits(Hour) + ":" + FormatTwoDigits(Minute) + " ";
        }

        return formattedHours;
    }

    private string FormatTwoDigits(int i)
    {
        string functionReturnValue = null;
        if (10 > i)
        {
            functionReturnValue = "0" + i.ToString();
        }
        else
        {
            functionReturnValue = i.ToString();
        }
        return functionReturnValue;
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