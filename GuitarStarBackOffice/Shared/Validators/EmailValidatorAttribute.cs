using System.ComponentModel.DataAnnotations;

namespace GuitarStarBackOffice.Shared.Validators;

public class EmailValidatorAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var email = (string)value;
        try
        {
            return EmailValidator.CheckIsValidEmail(email);
        }
        catch
        {
            return false;
        }
    }
}
