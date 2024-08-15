using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DVTChallenge.Enums
{
    public class ElevatorEnums
    {

        public enum Movement {
            [Description("Elevator going up")]
            Up=1,
            [Description("Elevator going down")]
            Down =2,
            [Description("Elevator is parked")]
           Parked = 2
        }

        public enum DoorOperation {
            [Description("Door opening")]
            Open = 1,
            [Description("Door Closing")]
            Close = 2,
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
