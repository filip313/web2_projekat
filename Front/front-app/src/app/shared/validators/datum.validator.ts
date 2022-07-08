import { AbstractControl } from "@angular/forms";

export function CustomValidacijaRodjenja(component : AbstractControl): {[key: string]: boolean} | null{
    const danasnjiDatum = Date.now();
    const datumRodjenja = component.value;
    let timeDiff = Math.abs(danasnjiDatum - datumRodjenja);
    let age = Math.floor((timeDiff/ (1000 * 3600 * 24))/365.25);
    return age < 18 ? { 'badAge':true} : null;
}