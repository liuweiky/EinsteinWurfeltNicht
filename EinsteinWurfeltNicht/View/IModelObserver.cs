using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EinsteinWurfeltNicht.Model;

namespace EinsteinWurfeltNicht.View
{
    interface IModelObserver
    {
        void Update(IModelObservable observable);
    }
}
