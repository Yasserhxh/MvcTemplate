using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PDF_FicheTechnique
    {
        public FicheTechniqueBridgeModel fichetechnique { get; set; }
        public List<FicheTehcniqueProduitBaseModel> fichetechniqueBase { get; set; }
        public List<AllergeneModel> allergenes { get; set; }
    }
}
