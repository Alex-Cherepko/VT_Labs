using CherepkoLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cherepko.Tests
{
    public class TestData
    {

        public static List<Rod> GetRodsList()
        {
            return new List<Rod>
            {
                    new Rod{ RodId=1,RodGroupId=1},
                    new Rod{ RodId=2,RodGroupId=1},
                    new Rod{ RodId=3,RodGroupId=2},
                    new Rod{ RodId=4,RodGroupId=3},
                    new Rod{ RodId=5,RodGroupId=3},
            };
        }
        public static IEnumerable<object[]> Params()
        {
            // 1-я страница, кол. объектов 3, id первого объекта 1
            yield return new object[] { 1, 3, 1 };
            // 2-я страница, кол. объектов 2, id первого объекта 4
            yield return new object[] { 2, 2, 4 };
        }

    }
}
