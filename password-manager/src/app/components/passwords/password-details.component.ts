import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PasswordService } from '../../password.service';
import { Password } from '../../models/password';
import { PasswordDetails } from '../../models/passwordDetails';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-password-detail',
  templateUrl: './password-details.component.html',
  imports: [CommonModule],
})
export class PasswordDetailComponent implements OnInit {

  password: PasswordDetails | null = null;
  errorMessage: string | null = null;

  constructor(
    private router: Router,
    private passwordService: PasswordService,
    private route: ActivatedRoute  // ActivatedRoute pour obtenir l'ID de l'URL
  ) {}

  ngOnInit(): void {
    const passwordId = +this.route.snapshot.paramMap.get('id')!;  // Récupérer l'ID du paramètre de l'URL
    this.loadPasswordDetail(passwordId);
  }

  loadPasswordDetail(id: number): void {
    this.passwordService.getPasswordById(id).subscribe(
      (password) => {
        this.password = password;
        console.log(password);
        
      },
      (error) => {
        this.errorMessage = 'Erreur lors du chargement des détails du mot de passe.';
        console.error(error);
      }
    );
  }

  deletePassword(): void {
    const password = this.password;
    if (!password) return;
    if (confirm('Êtes-vous sûr de vouloir supprimer ce mot de passe ?')) {
      this.passwordService.deletePassword(password.id).subscribe(
        () => {
          this.router.navigate(['/passwords']);
        },
        (error) => {
          this.errorMessage = 'Erreur lors de la suppression du mot de passe.';
          console.error(error);
        }
      );
    }
  }
}
