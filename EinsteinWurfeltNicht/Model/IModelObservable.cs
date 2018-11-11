using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EinsteinWurfeltNicht.View;

namespace EinsteinWurfeltNicht.Model
{
    interface IModelObservable
    {
        void Attatch(IModelObserver observer);
        void Notify();
    }
}
