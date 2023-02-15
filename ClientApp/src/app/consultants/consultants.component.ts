import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-consultants',
  templateUrl: './consultants.component.html'
})
export class ConsultantsComponent {
  public consultants: Consultant[] = [];
  private http: HttpClient;
  private baseUrl: string;
  public addMode = false;
  public editMode = false;
  public editConsultant = 0;
  private originalConsultant: Consultant = {
    id: 0,
    firstName: '',
    lastName: '',
    startDate: new Date()
  };
  public newConsultant: Consultant = {
    id: 0,
    firstName: '',
    lastName: '',
    startDate: new Date()
  };

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.http = http;
    this.baseUrl = baseUrl;
    http.get<Consultant[]>(baseUrl + 'consultant/Get').subscribe(result => {
      this.consultants = result;
    }, error => console.error(error));
  }

  public Remove(id: number) {
    this.http.delete(this.baseUrl + 'consultant/Remove/?id=' + id).subscribe(() => {
      this.consultants = this.consultants.filter(c => c.id != id);
    }, error => console.error(error));
  }

  public Update(consultant: Consultant, reset: boolean) {
    if (reset) {
      consultant.firstName = this.originalConsultant.firstName;
      consultant.lastName = this.originalConsultant.lastName;
      consultant.startDate = this.originalConsultant.startDate;
    } else {
      this.http.put(this.baseUrl + 'consultant/Update/' + consultant.id, consultant)
      .subscribe(() => {
        const index = this.consultants.findIndex(c => c.id === consultant.id);
        this.consultants[index] = consultant;
      }, error => console.error(error));
    } this.editMode = false;
  }

  public Edit(consultant: Consultant) {
    this.originalConsultant = {...consultant};
    this.editMode = true;
    this.editConsultant = consultant.id;
  }

  public Add(newConsultant: Consultant) {
    this.http.post<Consultant>(this.baseUrl + 'consultant/Add', newConsultant).subscribe((result) => {
        this.consultants.push(result);
      }, error => console.error(error));
  }
}

interface Consultant {
  id: number;
  firstName: string;
  lastName: string;
  startDate: Date;
}

