
# Create the resource group to hold the storage account
resource "azurerm_resource_group" "rg" {
    name     = var.resource_namer
    location = var.resource_group_location
}