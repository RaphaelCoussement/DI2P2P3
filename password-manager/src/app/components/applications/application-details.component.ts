import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApplicationService } from '../../application.service';
import { Application, ApplicationType } from '../../models/application';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-application-detail',
  templateUrl: './application-details.component.html',
  imports: [CommonModule],
})
export class ApplicationDetailsComponent implements OnInit {
  application: Application | null = null;
  errorMessage: string | null = null;
  applicationType = ApplicationType;    

  constructor(
    private route: ActivatedRoute,
    private applicationService: ApplicationService
  ) {}

  ngOnInit(): void {
    const appId = +this.route.snapshot.paramMap.get('id')!; // Récupération de l'ID de l'application depuis l'URL
    this.loadApplicationDetails(appId);
  }

  loadApplicationDetails(id: number): void {
    this.applicationService.getAppById(id).subscribe(
      (application) => {
        this.application = application;
      },
      (error) => {
        this.errorMessage = 'Erreur lors du chargement des détails de l\'application.';
        console.error(error);
      }
    );
  }
}
