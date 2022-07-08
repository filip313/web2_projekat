import { FormGroup, ValidationErrors } from "@angular/forms";

export function CustomPasswordValidator(control: FormGroup): ValidationErrors | null{
    const password = control.get('password');
    const passwordConfirm = control.get('passwordConfirm');

    return password?.value != passwordConfirm?.value?
        {'password-missmatch' : true} : null;
}