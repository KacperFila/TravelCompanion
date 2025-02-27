export class User {
  constructor(
    public email: string,
    public id: string,
    public role: string,
    private _token: string,
    private _claims: { permissions: string[]; activePlanId: string[] },
    private expirationDate: Date
  ) {}

  get token(): string | null {
    if (Date.now() > this.expirationDate.getTime()) {
      return null;
    }
    return this._token;
  }

  get activePlanId(): string {
    return this._claims.activePlanId[0];
  }

  set activePlanId(newActivePlanId: string) {
    this._claims.activePlanId[0] = newActivePlanId;
  }
}
