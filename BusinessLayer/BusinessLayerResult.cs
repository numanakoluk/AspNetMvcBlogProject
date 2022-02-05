using EntiyLayers.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BusinessLayerResult<T> where T: class
    {
        public List<KeyValuePair<ErrorMessageCode,string>>Errors { get; set; } //Bir liste KeyValuePair ile 2 değer alabilir hale geliyor.
        public T Result { get; set; }

        public BusinessLayerResult()
        {
            Errors = new List<KeyValuePair<ErrorMessageCode, string>>();
        }
        public void AddError(ErrorMessageCode code, string message)
        {
            Errors.Add(new KeyValuePair<ErrorMessageCode, string>(code, message));
        }
    }
}
