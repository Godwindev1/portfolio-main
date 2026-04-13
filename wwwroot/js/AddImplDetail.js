// ── addImplDetail ────────────────────────────────────────────────────────────
// Mirrors addUploadCard() but hard-wired to the ImplementationDetails prefix
// so the card structure always matches what the partial renders for existing items.
// Place this alongside the other add* functions in SaveCaseStudy.cshtml's <script>.

// ═══════════════════════════════════════════════════════════════════════════════
// INTEGRATION NOTES — SaveCaseStudy.cshtml
// ═══════════════════════════════════════════════════════════════════════════════

/*
1. WHERE TO PLACE THE PARTIAL
   ───────────────────────────
   Insert inside the <div class="full-width"> block, immediately AFTER the
   closing </div> of the section 08 // ARTIFACTS_&_MEDIA section and BEFORE
   the outer </div><!-- / full width --> comment:

       ...
       </div><!-- / section 08 -->

       <%-- NEW --%>
       <partial name="_ImplementationDetailArtifacts" model="Model" />

   </div><!-- / full width -->


2. VIEWMODEL — add this property to SaveCaseStudyViewModel
   ─────────────────────────────────────────────────────────
   public List<ImplementationDetailArtifactViewModel> ImplementationDetails { get; set; }
       = new();

   And create the sub-ViewModel:

   public class ImplementationDetailArtifactViewModel
   {
       public int?   Id          { get; set; }
       public int?   StepOrder   { get; set; }   // optional visual grouping hint
       public string Label       { get; set; } = "";
       public string ExistingUrl { get; set; } = "";
       public IFormFile? File    { get; set; }
   }


3. HANDLER MAPPING (PageModel / Controller)
   ──────────────────────────────────────────
   The binding name "ImplementationDetails" matches the property name above,
   so standard [BindProperty] / model binding picks it up automatically.
   In your Save handler, loop ImplementationDetails the same way you loop
   Screenshots — upload the file if File != null, otherwise keep ExistingUrl.
   Set ArtifactType = ArtifactTypes.ImplementationDetail when persisting.
*/
