using Newtonsoft.Json;
using Sally.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SallyProviderExample.Models
{
    [Serializable]
    public class Appointment : EntityValue
    {

        //
        //Variables
        //
        [JsonIgnore]
        public String Subject
        {
            get
            {
                return (GetFieldValueByLogicalName("Subject").Value as StringValue).Value;
            }
            set
            {
                (GetFieldValueByLogicalName("Subject").Value as StringValue).Value = value;
            }
        }

        [JsonIgnore]
        public List<Person> Attendees
        {
            get
            {
                return GetFieldValueByLogicalName("Attendees").Values.Select(x => new Person((EntityValue)x)).ToList();
            }
            set
            {
                GetFieldValueByLogicalName("Attendees").Values = value?.Select(x => (IValue)x).ToList();
            }
        }

        [JsonIgnore]
        public DateTime? StartDate 
        {
            get
            {
                return (GetFieldValueByLogicalName("StartDate").Value as DateTimeValue)?.SingleDateTime;
            }
            set
            {
                (GetFieldValueByLogicalName("StartDate").Value as DateTimeValue).SingleDateTime = value;
            }
        }

        [JsonIgnore]
        public DateTime? EndDate
        {
            get
            {
                return (GetFieldValueByLogicalName("EndDate").Value as DateTimeValue)?.SingleDateTime;
            }
            set
            {
                (GetFieldValueByLogicalName("EndDate").Value as DateTimeValue).SingleDateTime = value;
            }
        }

        //
        //Constructor
        //
        public Appointment() : base()
        {
            CreateFields();
            LogicalName = "Appointment";
        }

        public Appointment(EntityValue EValue) : base()
        {
            this.ID = EValue.ID;
            this.LogicalName = EValue.LogicalName;
            this.Name = EValue.Name;
            this.Fields = EValue.Fields;
            this.Sources = EValue.Sources;
        }

        //
        //Public Functions
        //
        public ValueContainer GetFieldValueByLogicalName(String LogicalName)
        {
            return Fields.Where(x => x.LogicalName == LogicalName).FirstOrDefault()?.Value;
        }

        //
        //Private Functions
        //
        private void CreateFields()
        {
            AddFieldIfNotExisting("Subject", new ValueContainer()
            {
                Value = new StringValue()
            });
            
            AddFieldIfNotExisting("Attendees", new ValueContainer()
            {
                IsList = true,
                Values = new List<IValue>()
            });

            AddFieldIfNotExisting("StartDate", new ValueContainer()
            {
                Value = new DateTimeValue()
            });

            AddFieldIfNotExisting("EndDate", new ValueContainer()
            {
                Value = new DateTimeValue()
            });

        }

    }
}