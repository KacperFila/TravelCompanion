export class User {
  constructor(
    public email: string,
    public id: string,
    public role: string,
    public activePlanId: string,
    private _token: string,
    private _claims: { permissions: string[] },
    private expirationDate: Date
  ) {}

  get token(): string | null {
    return this._token;
  }
}
