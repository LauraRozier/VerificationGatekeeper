*Verification Gatekeeper* Prevents players from doing anything on the server until they are member of a specific group.

This plugin was written after a suggestion by [TFNBlackMarket](https://umod.org/user/TFNBlackMarket)


Keep in mind that this plugin will disable every possible action a player can perform, other then walking around.


## Configuration

- **Verified Player Group** -- Defines the group that disables all the preventive measures of this plugin.
- **Admin Is Always Verified** -- (true/false) When set to "true" players with the Admin rank will bypass the preventive measures.

```json
{
  "Verified Player Group": "verified",
  "Admin Is Always Verified": true
}
```
