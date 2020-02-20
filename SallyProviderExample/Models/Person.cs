using Newtonsoft.Json;
using Sally.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SallyProviderExample.Models
{
    [Serializable]
    public class Person : EntityValue
    {

        //
        //Variables
        //
        [JsonIgnore]
        public String FirstName 
        {
            get
            {
                return (GetFieldValueByLogicalName("FirstName").Value as StringValue).Value;
            }
            set
            {
                (GetFieldValueByLogicalName("FirstName").Value as StringValue).Value = value;
            }
        }

        [JsonIgnore]
        public String LastName
        {
            get
            {
                return (GetFieldValueByLogicalName("LastName").Value as StringValue).Value;
            }
            set
            {
                (GetFieldValueByLogicalName("LastName").Value as StringValue).Value = value;
            }
        }

        [JsonIgnore]
        public String Company
        {
            get
            {
                return (GetFieldValueByLogicalName("Company").Value as StringValue).Value;
            }
            set
            {
                (GetFieldValueByLogicalName("Company").Value as StringValue).Value = value;
            }
        }

        [JsonIgnore]
        public String Notes 
        {
            get
            {
                return (GetFieldValueByLogicalName("Notes").Value as StringValue).Value;
            }
            set
            {
                (GetFieldValueByLogicalName("Notes").Value as StringValue).Value = value;
            }
        }

        [JsonIgnore]
        public String EntityLogicalName
        {
            get
            {
                return (GetFieldValueByLogicalName("EntityLogicalName").Value as StringValue).Value;
            }
            set
            {
                (GetFieldValueByLogicalName("EntityLogicalName").Value as StringValue).Value = value;
            }
        }


        //
        //Constructor
        //
        public Person() : base()
        {
            CreateFields();
            LogicalName = "Person";
        }

        public Person(EntityValue EValue) : base()
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
            AddFieldIfNotExisting("FirstName", new ValueContainer()
            {
                Value = new StringValue()
            });

            AddFieldIfNotExisting("LastName", new ValueContainer()
            {
                Value = new StringValue()
            });

            AddFieldIfNotExisting("Company", new ValueContainer()
            {
                Value = new StringValue()
            });

            AddFieldIfNotExisting("Notes", new ValueContainer()
            {
                Value = new StringValue()
            });

            AddFieldIfNotExisting("EntityLogicalName", new ValueContainer()
            {
                Value = new StringValue()
            });


        }

    }
}