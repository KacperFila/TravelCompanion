import { Component } from "@angular/core";
import { FormsModule, NgForm } from "@angular/forms";
import { AuthService } from "./auth.service";

@Component({
    standalone: true,
    selector: 'app-auth',
    templateUrl: './auth.component.html',
    imports: [FormsModule]
})
export class AuthComponent
{
    constructor(private authService: AuthService) {}

    onSubmit(form: NgForm)
    {
        const email = form.value.email;
        const password = form.value.password;
        
        this.authService.signup(email, password).subscribe(response => {
            console.log(response)
        }, error => {
            console.log(error)
        });

        console.log(form)
    }
}