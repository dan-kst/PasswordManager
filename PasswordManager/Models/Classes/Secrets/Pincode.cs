using System.ComponentModel.DataAnnotations;
using PasswordManager.Models.Enums;

namespace PasswordManager.Models.Classes.Secrets
{
    public class Pincode : SecretBase
    {
        [Required]
        [Display(Name = "Name")]
        public override string? Name { get { return base.Name; } set { base.Name = value; } }

        [Required]
        [Display(Name = "PIN")]
        public override string? Value { get { return base.Value; } set { base.Value = value; } }
        
        [Display(Name = "Secret Type")]
        public override EnumSecretType SecretType { get { return base.SecretType; } set { base.SecretType = value; } }
        
        
        public Pincode() : base()
        {
            SecretType = EnumSecretType.Pincode;
        }
    }
}
