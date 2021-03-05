using System.ComponentModel.DataAnnotations;

namespace BootstrapWebApplication.Models
{
    public class ApplicationUserSettingsViewModel
    {
        //[Required]
        [Display(Name = "Printer", ResourceType = typeof(BootstrapResources.Resources))]
        public string LabelPrinterName { get; set; }
    }

    public class AppSettingsViewModel
    {
        [Display(Name = "PrintServerUrl", ResourceType = typeof(BootstrapResources.Resources))]
        public string PrintServerUrl { get; set; }

        [Display(Name = "HolidayPayPercentage", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? HolidayPayPercentage { get; set; }

        [Display(Name = "MaternityPaymentPercentage", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? MaternityPaymentPercentage { get; set; }

        [Display(Name = "LaborInsurancePercentage", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? LaborInsurancePercentage { get; set; }

        [Display(Name = "MemberFinancialAidPaymentPerDay", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? MemberFinancialAidPaymentPerDay { get; set; } // InsuranceBankPaymentPerDay

        [Display(Name = "LaborInsuranceAmountPerDay", ResourceType = typeof(BootstrapResources.Resources))]
        public decimal? LaborInsuranceAmountPerDay { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(BootstrapResources.Resources))]
        public string Email { get; set; }

        [Display(Name = "Hometown", ResourceType = typeof(BootstrapResources.Resources))]
        public string HomeTown { get; set; }

        [Display(Name = "Birthday", ResourceType = typeof(BootstrapResources.Resources))]
        public System.DateTime? Birthday { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(25)]
        [Display(Name = "PhoneNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public string PhoneNumber { get; set; }

        [Display(Name = "UserIdCode", ResourceType = typeof(BootstrapResources.Resources))]
        public string UserIdCode { get; set; }

        public bool TwoFactorEnabled { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(BootstrapResources.Resources))]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(BootstrapResources.Resources))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessageResourceName = "MsgNewPasswordDontMatch",  ErrorMessageResourceType = typeof(BootstrapResources.Resources))]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        //[EmailAddress]
        [Display(Name = "EmailOrUserIdCode", ResourceType = typeof(BootstrapResources.Resources))]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(BootstrapResources.Resources))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(BootstrapResources.Resources))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(BootstrapResources.Resources))]
        public string Email { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(4)]
        [Display(Name = "UserIdCode", ResourceType = typeof(BootstrapResources.Resources))]
        public string UserIdCode { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(25)]
        [Display(Name = "PhoneNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(BootstrapResources.Resources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(BootstrapResources.Resources))]
        [Compare("Password", ErrorMessageResourceName = "MsgPasswordDontMatch", ErrorMessageResourceType = typeof(BootstrapResources.Resources))]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(BootstrapResources.Resources))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(BootstrapResources.Resources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(BootstrapResources.Resources))]
        [Compare("MsgPasswordDontMatch", ErrorMessageResourceType = typeof(BootstrapResources.Resources))]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(BootstrapResources.Resources))]
        public string Email { get; set; }
    }
}
