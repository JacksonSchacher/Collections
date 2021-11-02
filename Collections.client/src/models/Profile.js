export class Profile {
  constructor(profileData = {}) {
    this.id = profileData.id
    this.name = profileData.name
    this.picture = profileData.picture
  }
}