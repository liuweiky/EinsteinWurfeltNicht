using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EinsteinWurfeltNicht.Model;

namespace EinsteinWurfeltNicht.View
{
    public interface IModelObserver
    {
        void Update(IModelObservable observable);
    }
}
