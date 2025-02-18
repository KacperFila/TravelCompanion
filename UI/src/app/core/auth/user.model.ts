export class User {
  constructor(
    public email: string,
    public id: string,
    public role: string,
    private _token: string,
    private _claims: { permissions: string[] },
    private expirationDate: Date
  ) {}

  get token(): string {
    //TODO validation of expiration
    return this._token;
  }
}
