
# create a random strng to be used for the name of the storage accounts
resource "random_string" "sa_name_publisher" {
  length  = 16
  upper   = false
  special = false
}

resource "random_string" "sa_name_listener" {
  length  = 16
  upper   = false
  special = false
}

# Create a new storage accounts to store TF state for future deployments
resource "azurerm_storage_account" "sa_publisher" {
  name                = random_string.sa_name_publisher.result
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location

  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_storage_account" "sa_listener" {
  name                = random_string.sa_name_listener.result
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location

  account_tier             = "Standard"
  account_replication_type = "LRS"
}

# The app plans for the functions
resource "azurerm_app_service_plan" "app_sp_publisher" {
  name                = "service-plan-${var.function-publisher-name}"
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location

  sku {
    tier = "Standard"
    size = "S1"
  }
}

resource "azurerm_app_service_plan" "app_sp_listener" {
  name                = "service-plan-${var.function-listener-name}"
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location

  sku {
    tier = "Standard"
    size = "S1"
  }
}

# The function apps
resource "azurerm_function_app" "function_publisher" {
  name                = var.function-publisher-name
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location

  app_service_plan_id        = azurerm_app_service_plan.app_sp_publisher.id
  storage_account_name       = azurerm_storage_account.sa_publisher.name
  storage_account_access_key = azurerm_storage_account.sa_publisher.primary_access_key
}

resource "azurerm_function_app" "function_listener" {
  name                = var.function-listener-name
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location

  app_service_plan_id        = azurerm_app_service_plan.app_sp_listener.id
  storage_account_name       = azurerm_storage_account.sa_listener.name
  storage_account_access_key = azurerm_storage_account.sa_listener.primary_access_key
}

# Data for the function apps
data "azurerm_function_app_host_keys" "publisher" {
  depends_on = [
    azurerm_function_app.function_publisher
  ]
  name                = azurerm_function_app.function_publisher.name
  resource_group_name = var.resource_group_name
}

data "azurerm_function_app_host_keys" "listener" {
  depends_on = [
    azurerm_function_app.function_listener
  ]
  name                = azurerm_function_app.function_listener.name
  resource_group_name = var.resource_group_name
}
