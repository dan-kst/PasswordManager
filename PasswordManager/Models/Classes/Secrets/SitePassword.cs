using System.ComponentModel.DataAnnotations;
using PasswordManager.Models.Enums;

namespace PasswordManager.Models.Classes.Secrets
{
    public class SitePassword : SecretBase
    {
        [Required]
        [Display(Name = "Name")]
        public override string? Name { get { return base.Name; } set { base.Name = value; } }
        
        [Required]        
        [Display(Name = "Password")]
        public override string? Value { get { return base.Value; } set { base.Value = value; } }
        
        [Display(Name = "Type")]
        public override EnumSecretType SecretType { get { return base.SecretType; } set { base.SecretType = value; } }
        
        [Display(Name = "Password Strength")]
        public EnumSecretQuality SecretQuality { get; set; }
        
        [Display(Name = "URL")]
        public string? SiteURL { get; set; }
        public SitePassword() : base()
        {
            SecretType = EnumSecretType.SitePassword;
            SecretQuality = EnumSecretQuality.Weak;
        }
    }
}
