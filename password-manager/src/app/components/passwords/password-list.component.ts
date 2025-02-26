import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { PasswordService } from '../../password.service';
import { Password } from '../../models/password';
import { ApplicationService } from '../../application.service';
import { Application } from '../../models/application';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Modal } from 'bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { Router } from '@angular/router';

@Component({
  selector: 'app-password-list',
  templateUrl: './password-list.component.html',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  styleUrls: ['./password-list.component.css']
})
export class PasswordListComponent implements OnInit {

  passwords: Password[] = [];
  applications: Application[] = [];
  newPasswordForm: FormGroup;
  errorMessage: string | null = null;

  constructor(
    private router: Router,
    private passwordService: PasswordService,
    private applicationService: ApplicationService,
    private fb: FormBuilder
  ) {
    // Initialisation du formulaire pour ajouter un nouveau mot de passe
    this.newPasswordForm = this.fb.group({
      applicationId: ['', Validators.required],  // Application sélectionnée
      encryptedPassword: ['', [Validators.required, Validators.minLength(6)]]  // Mot de passe
    });
  }

  ngOnInit(): void {
    this.loadPasswords();
    this.loadApplications();
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

  // Charger les applications pour le select
  loadApplications(): void {
    this.applicationService.getApps().subscribe(
      (applications) => {
        this.applications = applications;
      },
      (error) => {
        this.errorMessage = 'Erreur lors du chargement des applications.';
        console.error(error);
      }
    );
  }

  // Soumettre le formulaire d'ajout de mot de passe
  onSubmit(): void {
    if (this.newPasswordForm.invalid) return;

    const newPassword = this.newPasswordForm.value;

    this.passwordService.addPassword(newPassword).subscribe({
      next: (password) => {
        this.passwords.push(password);
        this.newPasswordForm.reset();
        const modalElement = document.getElementById('addPasswordModal');
        if (modalElement) {
          const modalInstance = Modal.getInstance(modalElement) || new Modal(modalElement);
          modalInstance.hide();  // Fermer la modal après soumission
        }
        window.location.reload();
      },
      error: (error) => {
        this.errorMessage = 'Erreur lors de l\'ajout du mot de passe.';
        console.error(error);
      }
    });
  }
  viewPasswordDetail(passwordId: number): void {
    this.router.navigate([`/passwords/${passwordId}`]);  // Redirection vers le détail du mot de passe
  }
}
