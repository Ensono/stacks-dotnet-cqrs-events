
# create a random strng to be used for the name of the storage account
resource "random_string" "sa_name" {
    length = 16
    upper = false
    special = false
}

# Create a new storage account to store TF state for future deployments
resource "azurerm_storage_account" "sa" {
    count = var.function_count
    name = "${random_string.sa_name.result}${count.index}"
    resource_group_name = azurerm_resource_group.rg.name
    location = azurerm_resource_group.rg.location

    account_tier = "Standard"
    account_replication_type = "LRS"
}

# The app plan for the function
resource "azurerm_app_service_plan" "app_sp" {
    count = var.function_count
    name = "service-plan-${random_string.seed.result}-${count.index}"
    resource_group_name = azurerm_resource_group.rg.name
    location = azurerm_resource_group.rg.location

    sku {
        tier = "Standard"
        size = "S1"
    }
}

# The function app
resource "azurerm_function_app" "function" {
    count = var.function_count
    name = "function-${random_string.seed.result}-${count.index}"
    resource_group_name = azurerm_resource_group.rg.name
    location = azurerm_resource_group.rg.location

    app_service_plan_id = azurerm_app_service_plan.app_sp[count.index].id
    storage_account_name = azurerm_storage_account.sa[count.index].name
    storage_account_access_key = azurerm_storage_account.sa[count.index].primary_access_key
}