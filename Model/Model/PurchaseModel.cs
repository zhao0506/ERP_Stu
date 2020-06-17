using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
	 public class PurchaseModel
	 {
		 public int  ReceIptsId { get; set; }
		 public string  ReceIptsCode { get; set; }
		 public int  SId { get; set; }
		 public int  GId { get; set; }
		 public int  Number { get; set; }
		 public string  Rate { get; set; }
		 public string  Discount { get; set; }
		 public string  CMoney { get; set; }
		 public int  AId { get; set; }
		 public DateTime  Datetime { get; set; }
		 public int  PayMent { get; set; }
		 public string  Remark { get; set; }
		 public int  IsState { get; set; }
	 }
}
