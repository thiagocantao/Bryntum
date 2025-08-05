using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param
{
   public class ParamEnviaEmail
    {
        public int? CodigoEntidadeMaster { get; set; }
        public string EmailDestinatario { get; set; }
        public string EmailCopia { get; set; }
        public string AssuntoEmail { get; set; }
        public string Body { get; set; }
        public string Convite { get; set; }
        public string Anexo { get; set; }
    }
}
