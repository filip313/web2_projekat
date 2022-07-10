import { FormGroup, ValidationErrors } from "@angular/forms";

export function CustomPasswordValidator(control: FormGroup): {[key:string]:boolean}| null{
    const password = control.get('password')?.value;
    const passwordConfirm = control.get('passwordConfirm')?.value;

    if(password && passwordConfirm){
        if(password == passwordConfirm){
            return null;
        }else{
            return {'password-missmatch':true}
        }
    }
    return null;
}

export function CustomNewPasswordValidator(control: FormGroup):{[key:string]:boolean} | null{
    const password = control.get('noviPassword')?.value;
    const passwordConfirm = control.get('noviPasswordConfirm')?.value;

    if(password && passwordConfirm){
        if(password == passwordConfirm){
            return null;
        }else{
            return {'password-missmatch':true}
        }
    }
    return null;
}

