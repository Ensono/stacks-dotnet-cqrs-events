
# Create the resource group to hold the storage account
resource "azurerm_resource_group" "rg" {
    name = module.default_label.id
    location = var.resource_group_location
}