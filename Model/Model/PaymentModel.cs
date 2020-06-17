using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
	 public class PaymentModel
	 {
		 public int  PaymentId { get; set; }
		 public string  PaymentCode { get; set; }
		 public int  ClientId { get; set; }
		 public int  ReceIptsId { get; set; }
		 public string  CNumber { get; set; }
		 public int  Aid { get; set; }
		 public DateTime  RTime { get; set; }
		 public string  Remark { get; set; }
		 public int  IsState { get; set; }
	 }
}
