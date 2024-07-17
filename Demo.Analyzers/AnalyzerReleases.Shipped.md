## Release 1.0

### New Rules

Rule ID | Category | Severity | Notes                                                                                 
--------|----------|----------|--------------------------------------------------------------------------------------
DEM001  | Usage    | Warning  | Don't use the `null` keyword.                                                        
DEM002  | Usage    | Warning  | Don't use nullable types.                                                            
DEM003  | Usage    | Warning  | Use `DateTime.UtcNow` instead of `DateTime.Now`.                                     
DEM004  | Usage    | Warning  | Don't use `DateTime.UtcNow`. Use an injectable abstraction to better support testing. 