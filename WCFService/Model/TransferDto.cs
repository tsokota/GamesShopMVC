using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;


namespace WCFService.Model
{
    [DataContract]
    public class TransferDto
    {
        [DataMember]
        public Guid SenderAccount { get; set; }

        [DataMember]
        public Guid RecipientAccount { get; set; }

        [DataMember]
        public decimal Ammount { get; set; }

        /// <summary>
        /// was transfer succesful or not, failed transfers going to history too!
        /// </summary>
        [DataMember]
        public bool Succesfull { get; set; }
    }
}