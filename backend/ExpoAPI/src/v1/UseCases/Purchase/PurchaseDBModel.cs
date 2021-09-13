using System.Xml;
using System.Xml.Linq;

namespace ExpoAPI.UseCases.Purchase
{
    [Serializable]  
    public class PurchaseDBModel
    {
        public int PurchaseID { get; set; }
        public XmlDocument? Seller { get; set; }
        public XmlDocument? Purchaser { get; set; }
        public DateTime PurchaseDate { get; set; }
        public XmlDocument? PurchaseInfo { get; set; }
    }
}