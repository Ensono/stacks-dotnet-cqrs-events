
# Create the resource group to hold the storage account
resource "azurerm_resource_group" "rg" {
    count = var.create_resource_group == true ? 1 : 0
    name = module.default_label.id
    location = var.resource_group_location
}