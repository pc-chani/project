import { FormControl, ValidatorFn, FormGroup } from '@angular/forms';

export function AgeValidator(age: number): ValidatorFn {
    return (control: FormControl): { [Key: string]: any } | null => {
        if (control.value == null) return null;
        let startDate = new Date(control.value);
        const endDate = new Date();
        const acage = endDate.getFullYear() - startDate.getFullYear();
        if (endDate.getFullYear() - age < startDate.getFullYear())
            return { ageError: { actualAge: acage, requiredAge: age }, difference: age - acage };
        return null;
    }
}

export function CheckPassword(pass1: string, pass2: string): ValidatorFn {
    return (form: FormGroup): { [Key: string]: any } | null => {
        if (form.controls[pass1].value != form.controls[pass2].value) 
        return {passwordError: 'האימות נכשל'};
        return null;
    }
    }
    export function aPhone(phone1: string, phone2: string): ValidatorFn {
        return (form: FormGroup): { [Key: string]: any } | null => {
            if (form.controls[phone1].value == '' && form.controls[phone2].value=='') 
            return {passwordError: 'חסר מספר טלפון'};
            return null;
        }
    }
    export function isCategory(c: string, tc: string): ValidatorFn {
        return (form: FormGroup): { [Key: string]: any } | null => { 
            if (form.controls[c].value == null && form.controls[tc].value==null  ||
                (form.controls[c].value == "" && form.controls[tc].value=="")||
                (form.controls[c].value == "" && form.controls[tc].value==null)||
                (form.controls[c].value == null && form.controls[tc].value=="")) 
            return {isCategory: 'לא נבחרה קטגוריה  '};
            return null;
        }
    }

    export function validSearch(p: string): ValidatorFn {
        return (form: FormGroup): { [Key: string]: any } | null => {  
            console.log(form.controls[p].value)  
            if (form.controls[p].disabled && form.controls[p].value==null) 
            return {searchError: 'לא נבחר מיקום  '};
            return null;
        }
    }
