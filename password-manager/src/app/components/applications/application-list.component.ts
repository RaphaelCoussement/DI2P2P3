import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ApplicationService } from '../../application.service';
import { Application, ApplicationType,  } from '../../models/application';
import { ApplicationDto } from '../../models/applicationDto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { Modal } from 'bootstrap'

@Component({
  selector: 'app-application-list',
  templateUrl: './application-list.component.html',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  styleUrls: ['./application-list.component.css']
})
export class ApplicationListComponent implements OnInit {

  applications: Application[] = [];
  newApplicationForm: FormGroup;
  newApplication: ApplicationDto = { name: '', type: ApplicationType.GrandPublic };
  isAddingApplication = false;
  errorMessage: string | null = null;
  applicationType = ApplicationType;

  constructor(
    private router: Router,
    private applicationService: ApplicationService,
    private fb: FormBuilder
  ) {
    // Initialisation du formulaire pour ajouter une nouvelle application
    this.newApplicationForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      type: [ApplicationType.GrandPublic, Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadApplications();
  }

  // Charger les applications depuis le service
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

  onSubmit(): void {
    if (this.newApplicationForm.invalid) return;

    const newApp: ApplicationDto = this.newApplicationForm.value;
    this.isAddingApplication = true;
    this.errorMessage = null;

    this.applicationService.addApp(newApp).subscribe({
      next: (app) => {
        this.applications.push(app);
        this.newApplicationForm.reset(); 
        this.isAddingApplication = false;

        const modalElement = document.getElementById('addApplicationModal');
      if (modalElement) {
        const modalInstance = Modal.getInstance(modalElement) || new Modal(modalElement);
        modalInstance.hide();
      }
      
      },
      error: (error) => {
        this.errorMessage = 'Erreur lors de l\'ajout de l\'application.';
        console.error(error);
        this.isAddingApplication = false;
      }
    });
  }
  
}
