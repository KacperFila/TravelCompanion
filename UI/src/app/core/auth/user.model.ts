import { TravelPlan } from '../features/plans/models/plan.models';

export class User {
  public activePlan: TravelPlan | null = null;

  constructor(
    public email: string,
    public id: string,
    public role: string,
    private _token: string,
    private _claims: { permissions: string[] },
    private expirationDate: Date
  ) {}

  get token(): string | null {
    return this._token;
  }

  getActivePlan(): TravelPlan | null {
    return this.activePlan;
  }
}
