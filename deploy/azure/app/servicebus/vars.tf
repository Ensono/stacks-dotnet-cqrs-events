# Required variables

variable "resource_group_location" {
    type = string
    description = "Region in Azure that the resources should be deployed to"
}

variable "topic_name" {
    type = string
    description = "Name of the topic to create"
}

variable "subscription_name" {
    type = string
    description = "Name of the Service Bus subscription to create"
}

variable "subscription_filtered_name" {
    type = string
    description = "Name of the Service Bus subscription, filtered, to create"
}

# Optional variables
# These have default values that can be overriden as required
variable "seed_length" {
    type = number
    default = 4
}

variable "servicebus_sku" {
    type = string
    default = "Standard"
}

variable "function_count" {
    type = string
    default = 1
}

variable "location_name_map" {
  type = map(string)

  default = {
    northeurope   = "eun"
    westeurope    = "euw"
    uksouth       = "uks"
    ukwest        = "ukw"
    eastus        = "use"
    eastus2       = "use2"
    westus        = "usw"
    eastasia      = "ase"
    southeastasia = "asse"
  }
}