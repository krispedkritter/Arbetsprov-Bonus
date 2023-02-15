import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-bonus',
  templateUrl: './bonus.component.html',
})
export class BonusComponent {
  public consultants$: Observable<Consultant[]>;
  private consultants: Consultant[] = [];
  public bonuses: ConsultantBonus[] = [];
  public netResult = 0;
  public netDeductingPoints = 0;
  private bonusPot = 0;

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
      this.consultants$ = http.get<Consultant[]>(baseUrl + 'consultant/Get');

      this.consultants$.subscribe(result => {
        result.forEach(consultant => {
          this.consultants.push(consultant);
          const yearsEmployed = this.calculateYearsEmployed(consultant.startDate);
          const loyaltyFactor = this.calculateLoyalty(yearsEmployed);  
          let deductedHours = 0; 
          const deductingPoints = deductedHours * loyaltyFactor;
          let bonus = 0;
  
          this.bonuses.push({
            id: consultant.id,
            yearsEmployed,
            loyaltyFactor,
            bonus,
            deductedHours,
            deductingPoints
          });
        });
        
      }, error => console.error(error));
  }
  public calculateLoyalty(yearsEmployed: number){
    if(yearsEmployed < 1){
      return 1;
    } else if(yearsEmployed >= 1 && yearsEmployed < 5){
      return (1 + (0.1 * yearsEmployed))
    } else {
      return 1.5;
    }
  }

  public calculateYearsEmployed(startDate: Date): number {
    return (Math.round((new Date().getTime() - new Date(startDate).getTime()) / (1000 * 3600 * 24 * 365)));
  }

  public updateAllFactors() {
    this.bonusPot = this.netResult * 0.05;
    for (let i = 0; i < this.consultants.length; i++) {
      const consultant = this.consultants[i];
      const yearsEmployed = this.calculateYearsEmployed(consultant.startDate);
      const loyaltyFactor = this.calculateLoyalty(yearsEmployed);
      const deductedHours = this.bonuses[i].deductedHours;
      const deductingPoints = deductedHours * loyaltyFactor;
      
      this.bonuses[i].deductingPoints = deductingPoints;
      this.bonuses[i].yearsEmployed = yearsEmployed;
      this.bonuses[i].loyaltyFactor = loyaltyFactor;
    }
    this.calculateBonuses();
  }

  public calculateBonuses() {
    this.netDeductingPoints = 0;
    this.bonusPot = this.netResult * 0.05;
    if (this.netResult !== 0) {
      for (let i = 0; i < this.consultants.length; i++) {
        if (this.bonuses[i].deductedHours !== 0) {
          this.netDeductingPoints += this.bonuses[i].deductedHours * this.bonuses[i].loyaltyFactor;
        }
      }
      for (let i = 0; i < this.consultants.length; i++) {
        if (this.bonuses[i].deductedHours !== 0) {
          this.bonuses[i].deductingPoints = this.bonuses[i].deductedHours * this.bonuses[i].loyaltyFactor;
          this.bonuses[i].bonus = Math.round((this.bonuses[i].deductingPoints / this.netDeductingPoints) * this.bonusPot);
        }
      }
    }
  }

}
interface Consultant {
  id: number;
  firstName: string;
  lastName: string;
  startDate: Date;
}

interface ConsultantBonus {
  id: number;
  yearsEmployed: number;
  loyaltyFactor: number;
  bonus: number;
  deductedHours: number;
  deductingPoints: number;
}