import PackageRequest from "@/classes/requests/PackageRequest";

export default class PackageValidation {

  public SenderHasValue: Boolean = true;
  public NameHasValue: Boolean = true;

  public CollectionPointIdHasValue: Boolean = true;
  public CreatedAtLocationIdHasValue: Boolean = true;

  public CreatedByPersonIdHasValue: Boolean = true;
  public ReceiverIdHasValue: Boolean = true;


  public CollectionPointIdValid: Boolean = true;
  public CreatedAtLocationIdValid: Boolean = true;

  public CreatedByPersonIdValid: Boolean = true;
  public ReceiverIdValid: Boolean = true;

  public fieldsHaveValues(request: PackageRequest) {
    
    // Set model fields for displaying errors.
    this.SenderHasValue = !!request.Sender;
    this.NameHasValue = !!request.Name;

    return (
      this.SenderHasValue && 
      this.NameHasValue);
  }

  public locationsCorrect() : Boolean {
    return (
      this.CollectionPointIdValid &&
      this.CreatedAtLocationIdValid
    )
  }

  public personsCorrect() : Boolean {
    return (
    this.ReceiverIdValid &&
    this.CreatedByPersonIdValid 
    )
  }
}