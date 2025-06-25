using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Models
{
    public class PredictionResponse
    {
        public string Text { get; set; }
        public string Prediction { get; set; }
        public float Confidence { get; set; }
        public List<float> Logits { get; set; }
    }

}
