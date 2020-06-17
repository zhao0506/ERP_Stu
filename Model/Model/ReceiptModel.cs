using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
	 public class ReceiptModel
	 {
		 public int  ReceiptId { get; set; }
		 public string  ReceiptCode { get; set; }
		 public int  ClientId { get; set; }
		 public int  ClearId { get; set; }
		 public string  CNumber { get; set; }
		 public int  Aid { get; set; }
		 public DateTime  RTime { get; set; }
		 public string  Remark { get; set; }
		 public int  IsState { get; set; }
	 }
}
