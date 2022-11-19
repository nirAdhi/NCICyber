using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models
{
    public class EventReg
    {
        [Required]
        public int Event1Id { get; set; }

        [EventValidation(2,nameof(Event1Id), nameof(Event2Id), nameof(Event3Id))]
        public int? Event2Id { get; set; }

        [EventValidation(3, nameof(Event1Id), nameof(Event2Id), nameof(Event3Id))]
        public int? Event3Id { get; set; }
        public decimal? TotalAmount { get; set; }
    }
    public class EventValidation : ValidationAttribute
    {
        private readonly string _event1property;
        public readonly string _event2property;
        public readonly string _event3property;
        public int _src;
        public  EventValidation(int src, string Event1id, string Event2id, string Event3id)
        {
           this. _src = src;
            this._event1property =  Event1id;
           this._event2property = Event2id;
            this._event3property = Event3id;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property1 = validationContext.ObjectType.GetProperty(_event1property);
            var property2 = validationContext.ObjectType.GetProperty(_event2property);
            var property3 = validationContext.ObjectType.GetProperty(_event3property);
            if (property1 == null)
                throw new ArgumentException($"Property {_event1property} not found");
            if (property2 == null)
                throw new ArgumentException($"Property {_event2property} not found");
            if (property3 == null)
                throw new ArgumentException($"Property {_event3property} not found");
            int id1 = Convert.ToInt32(property1.GetValue(validationContext.ObjectInstance));
            int id2 = Convert.ToInt32(property2.GetValue(validationContext.ObjectInstance));
            int id3 = Convert.ToInt32(property3.GetValue(validationContext.ObjectInstance));
            if (_src == 2)
            {
                if (id2 >0)
                {
                    if (id2 == id1)
                    {
                        return new ValidationResult("Event 2 can not be same as Event1.");
                    }
                    else if(id3>0 && id2 == id3)
                    {
                        return new ValidationResult("Event 2 can not be same as Event3.");
                    }
                    else if(id2 == id1 || id2 == id3)
                    {
                        return new ValidationResult("Event 2 can not be same as Event1 & Event3.");
                    }
                }
             
                
            }
            else if (_src == 3)
            {
                if (id3 > 0)
                {
                    if (id3 == id1)
                    {
                        return new ValidationResult("Event 3 can not be same as Event1.");
                    }
                    else if (id2 > 0 && id2 == id3)
                    {

                    }
                    else if (id3 == id1 || id3 == id2)
                    {
                        return new ValidationResult("Event 3 can not be same as Event 1 & Event 2.");
                    }
                }
               
            }
            return ValidationResult.Success;
        }
    }
}
