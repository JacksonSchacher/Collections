export class Keep {
  constructor(keepData = {}) {
    this.id = keepData.id
    this.createdAt = keepData.createdAt
    this.updatedAt = keepData.updatedAt
    this.creator = keepData.creator
    this.name = keepData.name
    this.description = keepData.description
    this.img = keepData.img
    this.views = keepData.views
    this.shares = keepData.shares
    this.keeps = keepData.keeps
  }
}