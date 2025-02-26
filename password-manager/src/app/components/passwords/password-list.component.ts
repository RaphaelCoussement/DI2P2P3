import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { PasswordService } from '../../password.service';
import { Password } from '../../models/password';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-password-list',
  templateUrl: './password-list.component.html',
  imports: [CommonModule],
  styleUrls: ['./password-list.component.css']
})
export class PasswordListComponent implements OnInit {

  passwords: Password[] = [];
  newPasswordForm: FormGroup;
  isAddingPassword = false;
  errorMessage: string | null = null;

  constructor(
    private passwordService: PasswordService,
    private fb: FormBuilder
  ) {
    // Initialisation du formulaire pour ajouter un nouveau mot de passe
    this.newPasswordForm = this.fb.group({
      applicationId: ['', Validators.required],  // ID de l'application
      encryptedPassword: ['', [Validators.required, Validators.minLength(6)]],  // Exemple de validation pour le mot de passe
    });
  }

  ngOnInit(): void {
    this.loadPasswords();
  }

  // Charger les mots de passe depuis le service
  loadPasswords(): void {
    this.passwordService.getPasswords().subscribe(
      (passwords) => {
        this.passwords = passwords;
      },
      (error) => {
        this.errorMessage = 'Erreur lors du chargement des mots de passe.';
        console.error(error);
      }
    );
  }

  // Ajouter un nouveau mot de passe
  addPassword(): void {
    if (this.newPasswordForm.valid) {
      const newPassword: Password = this.newPasswordForm.value;
      this.passwordService.addPassword(newPassword).subscribe(
        (response) => {
          this.passwords.push(response);  // Ajouter le mot de passe à la liste
          this.isAddingPassword = false;  // Cacher le formulaire d'ajout
          this.newPasswordForm.reset();  // Réinitialiser le formulaire
        },
        (error) => {
          this.errorMessage = 'Erreur lors de l\'ajout du mot de passe.';
          console.error(error);
        }
      );
    } else {
      this.errorMessage = 'Le mot de passe est requis et doit comporter au moins 6 caractères.';
    }
  }

  // Afficher le formulaire d'ajout de mot de passe
  toggleAddPasswordForm(): void {
    this.isAddingPassword = !this.isAddingPassword;
  }
}
