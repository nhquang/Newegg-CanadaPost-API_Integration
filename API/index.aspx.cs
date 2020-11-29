using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace API
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            table.Visible = false;
            table2.Visible = false;
            error.Visible = false;
            error2.Visible = false;
        }

        

        protected async void submit_Click(object sender, EventArgs e)
        {
            try
            {
                //error.Visible = false;
                //error2.Visible = false;

                var parameters = new Dictionary<string, string>();
                parameters.Add("sellerid", ConfigurationManager.AppSettings["sellerID"].Trim());
                parameters.Add("version", ConfigurationManager.AppSettings["version"].Trim());

                var headers = new Dictionary<string, string>();
                headers.Add("Authorization", ConfigurationManager.AppSettings["Authorization"].Trim());
                headers.Add("SecretKey", ConfigurationManager.AppSettings["SecretKey"].Trim());
                

                var body = new RequestModel();
                body.OperationType = "GetOrderInfoRequest";
                body.RequestBody = new RequestBody();
                body.RequestBody.PageIndex = "1";
                body.RequestBody.PageSize = "10";
                body.RequestBody.RequestCriteria = new RequestCriteria();
                body.RequestBody.RequestCriteria.OrderNumberList = new OrderNumberList(new string[1] { orderNo.Text });




                var request = await HttpRequest.createRequest(ConfigurationManager.AppSettings["NeweggURL"].Trim(), "PUT", parameters, headers, null, null, JsonConvert.SerializeObject(body));

                var data = JsonConvert.DeserializeObject<ResponseModel>(await HttpRequest.getData(request));
                
                if(data.NeweggAPIResponse.ResponseBody.OrderInfoList.orderInfos.Length > 0)
                {
                    table.Visible = true;
                    
                }


            }
            catch(WebException wEx)
            {
                using (var stream = wEx.Response.GetResponseStream())
                {
                    using(var sr = new StreamReader(stream))
                    {
                        error.Text = await sr.ReadToEndAsync();
                        sr.Close();
                    }
                    stream.Close();
                }
                error.Visible = true;
                wEx.Response.Close();
                wEx.Response.Dispose();
            }
            catch (Exception ex)
            {
                error.Text = ex.Message;
                error.Visible = true;
            }
        }

        protected async void getrates_Click(object sender, EventArgs e)
        {
            try
            {
                //error.Visible = false;
                //error2.Visible = false;

                var username = ConfigurationSettings.AppSettings["username"].Trim();
                var password = ConfigurationSettings.AppSettings["password"].Trim();
                var mailedBy = ConfigurationSettings.AppSettings["mailedBy"].Trim();

                var url = ConfigurationManager.AppSettings["CanadaPostURL"].Trim();

                var method = "POST"; // HTTP Method

                // Create mailingScenario object to contain xml request
                mailingscenario mailingScenario = new mailingscenario();
                mailingScenario.parcelcharacteristics = new mailingscenarioParcelcharacteristics();
                mailingScenario.destination = new mailingscenarioDestination();
                mailingscenarioDestinationDomestic destDom = new mailingscenarioDestinationDomestic();
                mailingScenario.destination.Item = destDom;

                // Populate mailingScenario object
                mailingScenario.customernumber = mailedBy;
                mailingScenario.parcelcharacteristics.weight = Convert.ToDecimal(weight.Text);
                mailingScenario.originpostalcode = origin.Text.ToUpper().Trim().Replace(" ", "");

                //mailingScenario.contractid = ConfigurationManager.AppSettings["contractID"].Trim();


                destDom.postalcode = des.Text.ToUpper().Trim().Replace(" ","");

                StringBuilder mailingScenarioSb = new StringBuilder();
                XmlWriter mailingScenarioXml = XmlWriter.Create(mailingScenarioSb);
                mailingScenarioXml.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
                XmlSerializer serializerRequest = new XmlSerializer(typeof(mailingscenario));
                serializerRequest.Serialize(mailingScenarioXml, mailingScenario);

                //Headers
                var headers = new Dictionary<string, string>();
                headers.Add("Accept-Language", "en-CA");
                string auth = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
                headers.Add("Authorization", auth);


                var request = await HttpRequest.createRequest(url, method, null, headers, "application/vnd.cpc.ship.rate-v4+xml", "application/vnd.cpc.ship.rate-v4+xml", mailingScenarioSb.ToString());

                var data = await HttpRequest.getData(request);



                // Deserialize response to pricequotes object
                XmlSerializer serializer = new XmlSerializer(typeof(pricequotes));

                pricequotes priceQuotes = null;
                using (TextReader sr = new StringReader(data))
                {
                    priceQuotes = (pricequotes)serializer.Deserialize(sr);
                }
                

                // Retrieve values from pricequotes object
                foreach (var priceQuote in priceQuotes.pricequote)
                {
                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    cell1.Text = $"{priceQuote.servicename}";
                    cell3.Text = $"${Convert.ToDouble(priceQuote.pricedetails.due)}";
                    cell2.Text = $"{priceQuote.servicestandard.expectedtransittime}";
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell3);
                    table2.Rows.Add(row);
                }
                table2.Visible = true;
            }
            catch(WebException wEx)
            {
                
                using (var stream = wEx.Response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        error2.Text = await sr.ReadToEndAsync();
                        sr.Close();
                    }
                    stream.Close();
                }
                error2.Visible = true;
                wEx.Response.Close();
                wEx.Response.Dispose();
            }
            catch (Exception ex)
            {
                error.Text = ex.Message;
                error.Visible = true;
            }
        }
    }
}